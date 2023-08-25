using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Implementations.BookServices;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;
using LibraryManagementSystem.BLL.Tests.FakeData;
using LibraryManagementSystem.PL.Mapping;

namespace LibraryManagementSystem.BLL.Tests.ServiceTests
{
    public class BookServiceTests
    {
        private readonly IBookService _bookService;
    
        private readonly IMapper _mapper;
        private readonly Mock<IBookRepository> _mockedBookRepository;
        private readonly Mock<IStudentService> _mockedStudentService;

        public BookServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingBooksProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        
            _mockedBookRepository = new Mock<IBookRepository>();
            _mockedStudentService = new Mock<IStudentService>();

            _bookService = new BookService(_mapper, _mockedBookRepository.Object, _mockedStudentService.Object);
        }
    
        [Fact]
        public async Task GetBooksAsync_ShouldRetrieveAndMapBooks()
        {
            // Arrange
            int objectNumbersToGenerate = 20;
            var expectedBooksEntity = 
                BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);

            _mockedBookRepository.Setup(b => b.GetBooksAsync())
                .ReturnsAsync(expectedBooksEntity);

            var expectedBooksDto = 
                _mapper.Map<IEnumerable<BookEntity>, IEnumerable<BookSimpleDto>>(expectedBooksEntity);
        
            // Act
            var result = await _bookService.GetBooksAsync();
        
            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(objectNumbersToGenerate);
            result.Should().BeEquivalentTo(expectedBooksDto);
            result.Select(b => b.GenreIds).Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetBooksAsync_ShouldHandleEmptyList()
        {
            // Arrange
            _mockedBookRepository.Setup(b => b.GetBooksAsync())
                .ReturnsAsync(new List<BookEntity>());
        
            // Act
            var result = await _bookService.GetBooksAsync();

            // Assert
            result.Should().BeEmpty().And.NotBeNull();
        }

        [Theory]
        [InlineData("student1@gmail.com")]
        [InlineData("student2@gmail.com")]
        public async Task GetBooksFilteredByRoleAsync_ShouldRetrieveAndMapAndFilterBooks(string email)
        {
            // Arrange
            int objectNumbersToGenerate = 7;
            var expectedBooksEntity = 
                BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
            var expectedStudentDto = StudentFakeDataGenerator.GenerateStudentDto();

            _mockedBookRepository.Setup(b => b.GetBooksAsync())
                .ReturnsAsync(expectedBooksEntity);
            _mockedStudentService.Setup(s => s.GetStudentByEmailAsync(email))
                .ReturnsAsync(expectedStudentDto);

            var expectedBooksDto =
                _mapper.Map<IEnumerable<BookEntity>, IEnumerable<BookSimpleDto>>(expectedBooksEntity);
        
            // Act
            var result = await _bookService.GetBooksFilteredByRoleAsync(email);

            // Assert
            result.Should().NotBeNullOrEmpty().And.BeEquivalentTo(expectedBooksDto);
            result.Select(b => b.GenreIds).Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("unauthorized@gmail.com")]
        [InlineData("unauthenticated@gmail.com")]
        public async Task GetBooksFilteredByRoleAsync_ShouldHandleEmptyFavoriteGenres(string email)
        {
            // Arrange
            int objectNumbersToGenerate = 7;
        
            var expectedBooksEntity = BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
        
            var expectedStudentDto = StudentFakeDataGenerator.GenerateStudentDto();
            expectedStudentDto.FavoriteGenreIds = Array.Empty<int>();

            _mockedBookRepository.Setup(b => b.GetBooksAsync())
                .ReturnsAsync(expectedBooksEntity);
            _mockedStudentService.Setup(s => s.GetStudentByEmailAsync(email))
                .ReturnsAsync(expectedStudentDto);
        
            // Act
            var result = await _bookService.GetBooksFilteredByRoleAsync(email);

            // Assert
            result.Should().BeEmpty().And.NotBeNull();
        }
    
        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        public async Task GetBookByIdAsync_ShouldRetrieveAndMapBook(int bookId)
        {
            // Arrange
            var expectedBookEntity = BooksFakeDataGenerator.GenerateFakerBookEntity().Generate();
            expectedBookEntity.Id = bookId;

            _mockedBookRepository.Setup(b => b.GetBookByIdAsync(bookId))
                .ReturnsAsync(expectedBookEntity);

            var expectedBookDto = _mapper.Map<BookEntity, BookDto>(expectedBookEntity);
        
            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedBookDto);
            result!.Id.Should().Be(bookId);
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void GetBookByIdAsync_ShouldThrowException_IfInvalidId(int bookId)
        {
            // Arrange
        
            // Act
            Func<Task> act = async () => await _bookService.GetBookByIdAsync(bookId);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNull_IfBookEntityNotFound()
        {
            // Arrange
            BookEntity? expectedBookEntity = null;
            int bookId = new Random().Next(1, 2000); // must be non-negative

            _mockedBookRepository.Setup(b => b.GetBookByIdAsync(bookId))
                .ReturnsAsync(expectedBookEntity);
        
            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddBookAsync_ShouldReturnInsertedBookId_IfBookDoesntExist()
        {
            // Arrange
            int objectNumbersToGenerate = 20;
            int expectedInsertedId = 21;
            var bookDto = BooksFakeDataGenerator.GenerateBookDto();

            var expectedBooksEntity = BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
        
            _mockedBookRepository.Setup(b => b.GetBooksAsync()).ReturnsAsync(expectedBooksEntity);
            _mockedBookRepository.Setup(b => b.AddBookAsync(It.IsAny<BookEntity>()))
                .ReturnsAsync(expectedInsertedId);

            // Act
            int result = await _bookService.AddBookAsync(bookDto);

            // Assert
            result.Should().BeGreaterThan(0).And.Be(expectedInsertedId);
        }

        [Fact]
        public void AddBookAsync_ShouldThrowException_IfBookExists()
        {
            // Arrange
            int objectNumbersToGenerate = 20;
            var expectedBooksEntity = BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
            var bookDto = _mapper.Map<BookEntity, BookDto>(expectedBooksEntity.First());
        
            // Act
            Func<Task> act = async () => await _bookService.AddBookAsync(bookDto); 

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("This book already exists");
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldReturnTrue_IfBookWasUpdated()
        {
            // Arrange
            bool expectedUpdateResult = true;
            var bookDto = BooksFakeDataGenerator.GenerateBookDto();

            _mockedBookRepository.Setup(b => b.UpdateBookAsync(It.IsAny<BookEntity>()))
                .ReturnsAsync(expectedUpdateResult);
        
            // Act
            bool result = await _bookService.UpdateBookAsync(bookDto);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldReturnFalse_IfBookWasntUpdated()
        {
            // Arrange
            bool expectedUpdateResult = false;
            var bookDto = BooksFakeDataGenerator.GenerateBookDto();

            _mockedBookRepository.Setup(b => b.UpdateBookAsync(It.IsAny<BookEntity>()))
                .ReturnsAsync(expectedUpdateResult);
        
            // Act
            bool result = await _bookService.UpdateBookAsync(bookDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateBookAsync_ShouldThrowException_IfInvalidId()
        {
            // Arrange
            var bookDto = BooksFakeDataGenerator.GenerateBookDto();
            bookDto.Id = -1;

            // Act
            Func<Task> act = async () => await _bookService.UpdateBookAsync(bookDto);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }

        [Fact]
        public async Task DeleteBooksAsync_ShouldReturnTrue_IfBooksWereDeleted()
        {
            // Arrange
            var bookIds = new List<int> { 1, 3, 5 };
            bool expectedDeleteResult = true;

            _mockedBookRepository.Setup(b => b.DeleteBooksAsync(bookIds))
                .ReturnsAsync(expectedDeleteResult);
        
            // Act
            bool result = await _bookService.DeleteBooksAsync(bookIds);

            // Arrange
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBooksAsync_ShouldReturnFalse_IfBooksWerentDeleted()
        {
            // Arrange
            var bookIds = new List<int> { 505, 666, 10004 };
            bool expectedDeleteResult = false;

            _mockedBookRepository.Setup(b => b.DeleteBooksAsync(bookIds))
                .ReturnsAsync(expectedDeleteResult);
        
            // Act
            bool result = await _bookService.DeleteBooksAsync(bookIds);

            // Arrange
            result.Should().BeFalse();
        }

        [Fact]
        public void DeleteBooksAsync_ShouldThrowException_IfInvalidId()
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
        [InlineData(19)]
        public async Task DeleteBookByIdAsync_ShouldReturnTrue_IfBooksWereDeleted(int bookId)
        {
            // Arrange
            bool expectedDeleteResult = true;

            _mockedBookRepository.Setup(b => b.DeleteBookByIdAsync(bookId))
                .ReturnsAsync(expectedDeleteResult);

            // Act
            bool result = await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            result.Should().BeTrue();
        }
    
        [Theory]
        [InlineData(666)]
        [InlineData(1000)]
        public async Task DeleteBookByIdAsync_ShouldReturnFalse_IfBooksWerentDeleted(int bookId)
        {
            // Arrange
            bool expectedDeleteResult = false;

            _mockedBookRepository.Setup(b => b.DeleteBookByIdAsync(bookId))
                .ReturnsAsync(expectedDeleteResult);

            // Act
            bool result = await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            result.Should().BeFalse();
        }
    
        [Theory]
        [InlineData(-50)]
        [InlineData(-1)]
        [InlineData(0)]
        public void DeleteBookByIdAsync_ShouldThrowException_IfInvalidId(int bookId)
        {
            // Arrange
        
            // Act
            Func<Task> act = async () => await _bookService.DeleteBookByIdAsync(bookId);

            // Assert
            act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
        }
    }
}