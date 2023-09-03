namespace LibraryManagementSystem.IntegrationTests.ServiceTests
{
    public class BookServiceTests : IAsyncLifetime
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public BookServiceTests()
        {
            _dbContext = DbHelpers.GetApplicationDbContext("test");

            var services = new ServiceCollection();
            services.AddSingleton(_dbContext);
            services.AddAutoMapper(typeof(Program));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingBooksProfile>();
                cfg.AddProfile<MappingStudentsProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _bookService = new BookService(
                _mapper,
                serviceProvider.GetRequiredService<IBookRepository>(),
                serviceProvider.GetRequiredService<IStudentService>()
            );
        }

        public async Task InitializeAsync()
        {
            _dbContext.Books.AddRange(DbSeeder.GetPreconfiguredBooks());
            _dbContext.Students.AddRange(DbSeeder.GetPreconfiguredStudents());
            await _dbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Fact]
        public async Task GetBooksAsync_Should_Return_Books()
        {
            // Act
            var books = await _bookService.GetBooksAsync();

            // Assert
            books.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetBooksFilteredByStudentsFavoriteGenresAsync_Should_Return_Books_On_Correct_Email()
        {
            // Arrange
            string email = (await _dbContext.Students.FirstAsync()).Email;

            // Act
            var books = await _bookService.GetBooksFilteredByStudentsFavoriteGenresAsync(email);

            // Assert
            books.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetBooksFilteredByStudentsFavoriteGenresAsync_Should_Return_Empty_Books_On_Incorrect_Email()
        {
            // Arrange
            string email = "wrongEmail@gmail.com";

            // Act
            var books = await _bookService.GetBooksFilteredByStudentsFavoriteGenresAsync(email);

            // Assert
            books.Should().BeEmpty().And.NotBeNull();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        public async Task GetBookByIdAsync_Should_Return_Book_On_Correct_Id(int bookId)
        {
            // Act
            var book = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            book.Should().NotBeNull();
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void GetBookByIdAsync_Should_Throw_Exception_On_Incorrect_Id(int bookId)
        {
            // Act
            Func<Task> act = async () => await _bookService.GetBookByIdAsync(bookId);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }

        [Theory]
        [InlineData(666)]
        [InlineData(1000)]
        public async Task GetBookByIdAsync_Should_Return_Null_If_BookEntity_Not_Found(int bookId)
        {
            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddBookAsync_Should_Return_Inserted_BookId_If_New_Book()
        {
            // Arrange
            int expectedInsertedId = _dbContext.Books.Count() + 1;
            var bookDto = BookDtoFakeDataGenerator.GenerateFakerBookDto().Generate(1)[0];
            bookDto.Id = 0;

            // Act
            int result = await _bookService.AddBookAsync(bookDto);

            // Assert
            result.Should().BeGreaterThan(0).And.Be(expectedInsertedId);
        }

        [Fact]
        public async Task AddBookAsync_Should_Throw_Exception_If_Book_Exists()
        {
            // Arrange
            var bookEntity = await _dbContext.Books.FirstAsync();
            var bookDto = _mapper.Map<BookEntity, BookDto>(bookEntity);

            // Act
            Func<Task> act = async () => await _bookService.AddBookAsync(bookDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("This book already exists");
        }

        [Fact]
        public async Task UpdateBookAsync_Should_Return_True_If_Book_Was_Updated()
        {
           // Arrange
           var bookEntity = await _dbContext.Books.FirstAsync();
           var bookDto = _mapper.Map<BookEntity, BookDto>(bookEntity);
           bookDto.Title = "Secret Chamber";

           // Act
           bool result = await _bookService.UpdateBookAsync(bookDto);

           // Assert
           result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBookAsync_Should_Return_False_If_Book_Was_Not_Updated()
        {
            // Arrange
            var bookEntity = await _dbContext.Books.FirstAsync();
            var bookDto = _mapper.Map<BookEntity, BookDto>(bookEntity);

            // Act
            bool result = await _bookService.UpdateBookAsync(bookDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateBookAsync_Should_Throw_Exception_If_Invalid_Id()
        {
            // Arrange
            var bookEntity = await _dbContext.Books.FirstAsync();
            var bookDto = _mapper.Map<BookEntity, BookDto>(bookEntity);
            bookDto.Id = -1;

            // Act
            Func<Task> act = async () => await _bookService.UpdateBookAsync(bookDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }

        [Fact]
        public async Task DeleteBooksAsync_Should_Return_True_If_Books_Were_Deleted()
        {
            // Arrange
            var bookIds = new List<int> { 1, 3, 5 };

            // Act
            bool result = await _bookService.DeleteBooksAsync(bookIds);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBooksAsync_Should_Return_False_If_Books_Were_Not_Deleted()
        {
            // Arrange
            var bookIds = new List<int> { 505, 666, 10004 };

            // Act
            bool result = await _bookService.DeleteBooksAsync(bookIds);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void DeleteBooksAsync_Should_Throw_Exception_If_Invalid_Id()
        {
            // Arrange
            var bookIds = new List<int> { -3, 0, -1, 5 };

            // Act
            Func<Task> act = async () => await _bookService.DeleteBooksAsync(bookIds);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public async Task DeleteBookByIdAsync_Should_Return_True_If_Books_Were_Deleted(int bookId)
        {
            // Act
            bool result = await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(666)]
        [InlineData(1000)]
        public async Task DeleteBookByIdAsync_Should_Return_False_If_Books_Were_Not_Deleted(int bookId)
        {
            // Act
            bool result = await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void DeleteBookByIdAsync_Should_Throw_Exception_If_Invalid_Id(int bookId)
        {
            // Act
            Func<Task> act = async () => await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }
    }
}