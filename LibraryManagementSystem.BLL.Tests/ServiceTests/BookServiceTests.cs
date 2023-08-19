using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Implementations.BookServices;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;
using LibraryManagementSystem.BLL.Tests.FakeData;
using LibraryManagementSystem.PL.Mapping;

namespace LibraryManagementSystem.BLL.Tests.ServiceTests;

public class BookServiceTests
{
    private readonly IBookService _bookService;
    
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;
    private readonly Mock<IStudentService> _studentService;

    public BookServiceTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingBooksProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        
        _bookRepository = new Mock<IBookRepository>();
        _studentService = new Mock<IStudentService>();

        _bookService = new BookService(_mapper, _bookRepository.Object, _studentService.Object);
    }
    
    [Fact]
    public async Task GetBooksAsync_ShouldRetrieveAndMapBooks()
    {
        // Arrange
        int objectNumbersToGenerate = 20;
        var expectedBooksEntity = 
            BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);

        _bookRepository.Setup(b => b.GetBooksAsync())
            .ReturnsAsync(expectedBooksEntity);
        
        // Act
        var result = await _bookService.GetBooksAsync();
        
        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Should().HaveCount(objectNumbersToGenerate);
        result.Select(b => b.Description).Should().NotBeNull();
        result.Select(b => b.GenreIds).Should().NotBeNull();
        result.Select(b => b.GenreIds).Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetBooksAsync_ShouldHandleEmptyList()
    {
        // Arrange
        _bookRepository.Setup(b => b.GetBooksAsync())
            .ReturnsAsync(new List<BookEntity>());
        
        // Act
        var result = await _bookService.GetBooksAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
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

        _bookRepository.Setup(b => b.GetBooksAsync())
            .ReturnsAsync(expectedBooksEntity);
        _studentService.Setup(s => s.GetStudentByEmailAsync(email))
            .ReturnsAsync(expectedStudentDto);
        
        // Act
        var result = await _bookService.GetBooksFilteredByRoleAsync(email);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Select(b => b.GenreIds).Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("unauthorized@gmail.com")]
    public async Task GetBooksFilteredByRoleAsync_ShouldHandleEmptyFavoriteGenres(string email)
    {
        // Arrange
        int objectNumbersToGenerate = 7;
        
        var expectedBooksEntity = BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
        
        var expectedStudentDto = StudentFakeDataGenerator.GenerateStudentDto();
        expectedStudentDto.FavoriteGenreIds = Array.Empty<int>();

        _bookRepository.Setup(b => b.GetBooksAsync())
            .ReturnsAsync(expectedBooksEntity);
        _studentService.Setup(s => s.GetStudentByEmailAsync(email))
            .ReturnsAsync(expectedStudentDto);
        
        // Act
        var result = await _bookService.GetBooksFilteredByRoleAsync(email);

        // Assert
        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task AddBookAsync_ShouldReturnInsertedBookId_IfBookDoesntExist()
    {
        // Arrange
        int objectNumbersToGenerate = 20;
        var expectedInsertedId = 21;
        var bookDto = BooksFakeDataGenerator.GenerateBookDto();

        var expectedBooksEntity = BooksFakeDataGenerator.GenerateBooksEntity(objectNumbersToGenerate);
        
        _bookRepository.Setup(b => b.GetBooksAsync()).ReturnsAsync(expectedBooksEntity);
        _bookRepository.Setup(b => b.AddBookAsync(It.IsAny<BookEntity>()))
            .ReturnsAsync(expectedInsertedId);

        // Act
        int result = await _bookService.AddBookAsync(bookDto);

        // Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(expectedInsertedId);
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

        _bookRepository.Setup(b => b.UpdateBookAsync(It.IsAny<BookEntity>()))
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

        _bookRepository.Setup(b => b.UpdateBookAsync(It.IsAny<BookEntity>()))
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

        _bookRepository.Setup(b => b.DeleteBooksAsync(bookIds))
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

        _bookRepository.Setup(b => b.DeleteBooksAsync(bookIds))
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

    [Fact]
    public async Task DeleteBookByIdAsync_ShouldReturnTrue_IfBooksWereDeleted()
    {
        // Arrange
        int bookId = 13;
        bool expectedDeleteResult = true;

        _bookRepository.Setup(b => b.DeleteBookByIdAsync(bookId))
            .ReturnsAsync(expectedDeleteResult);

        // Act
        bool result = await _bookService.DeleteBookByIdAsync(bookId);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task DeleteBookByIdAsync_ShouldReturnFalse_IfBooksWerentDeleted()
    {
        // Arrange
        int bookId = 666;
        bool expectedDeleteResult = false;

        _bookRepository.Setup(b => b.DeleteBookByIdAsync(bookId))
            .ReturnsAsync(expectedDeleteResult);

        // Act
        bool result = await _bookService.DeleteBookByIdAsync(bookId);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void DeleteBookByIdAsync_ShouldThrowException_IfInvalidId()
    {
        // Arrange
        int bookId = -1;
        
        // Act
        Func<Task> act = async () => await _bookService.DeleteBookByIdAsync(bookId);

        // Assert
        act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be negative or zero");
    }
}