using AutoMapper;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentService _studentService;

        public BookService(
            IMapper mapper, 
            IBookRepository bookRepository, 
            IStudentService studentService)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _studentService = studentService;
        }

        public async Task<IEnumerable<BookSimpleDto>> GetBooksAsync()
        {
            var booksEntity = await _bookRepository.GetBooksAsync();
            var booksSimpleDto = _mapper.Map<IEnumerable<BookSimpleDto>>(booksEntity);

            return booksSimpleDto;
        }
        
        public async Task<IEnumerable<BookSimpleDto>> GetBooksFilteredByRoleAsync(string email)
        {
            var booksEntity = await _bookRepository.GetBooksAsync();
            
            var booksSimpleDto = _mapper.Map<IEnumerable<BookSimpleDto>>(booksEntity);
            var studentDto = await _studentService.GetStudentByEmailAsync(email);

            var booksFilteredDto = booksSimpleDto
                .Where(book => 
                    book.GenreIds
                        .Any(genreId => 
                            studentDto!.FavoriteGenreIds
                                .Contains(genreId)));

            return booksFilteredDto;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            ValidationHelper.ValidateId(id);

            var bookEntity = await _bookRepository.GetBookByIdAsync(id);
            if (bookEntity is not null)
            {
                var bookDto = _mapper.Map<BookDto>(bookEntity);
                return bookDto;    
            }

            return null;
        }

        public async Task<int> AddBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            var booksInDbEntity = await _bookRepository.GetBooksAsync();

            if (!booksInDbEntity.Any(bid => 
                    bid.Title == bookEntity.Title && 
                    bid.AuthorId == bookEntity.AuthorId))
            {
                return await _bookRepository.AddBookAsync(bookEntity);
            }

            throw new ArgumentException("This book already exists");
        }

        public async Task<bool> UpdateBookAsync(BookDto bookDto)
        {
            ValidationHelper.ValidateId(bookDto.Id);
            
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            return await _bookRepository.UpdateBookAsync(bookEntity);
        }

        public async Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            foreach (int id in bookIds)
            {
                ValidationHelper.ValidateId(id);
            }
            
            return await _bookRepository.DeleteBooksAsync(bookIds);
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            ValidationHelper.ValidateId(id);

            return await _bookRepository.DeleteBookByIdAsync(id);
        }
    }
}
