using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces;
using LibraryManagementSystem.BLL.Services.Interfaces;

namespace LibraryManagementSystem.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var booksEntity = await _bookRepository.GetBooksAsync();
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksEntity);

            return booksDto;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("BookId cannot be negative or zero");
            }

            var bookEntity = await _bookRepository.GetBookByIdAsync(id);
            var bookDto = _mapper.Map<BookDto>(bookEntity);

            return bookDto;
        }

        public async Task<int> AddBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            var booksInDbEntity = await _bookRepository.GetBooksAsync();

            if (!booksInDbEntity.Any(
                bid => bid.Title == bookEntity.Title && bid.AuthorId == bookEntity.AuthorId))
            {
                return await _bookRepository.AddBookAsync(bookEntity);
            }

            throw new ArgumentException("This book already exists");
        }

        public async Task<bool> UpdateBookAsync(BookDto bookDto)
        {
            if (bookDto.Id < 1)
            {
                throw new ArgumentException("BookId cannot be negative or zero");
            }
            
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            return await _bookRepository.UpdateBookAsync(bookEntity);
        }

        public async Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            if (bookIds.Any(bookId => bookId < 1))
            {
                throw new ArgumentException("BookId cannot be negative or zero");
            }
            
            return await _bookRepository.DeleteBooksAsync(bookIds);
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("BookId cannot be negative or zero");
            }

            return await _bookRepository.DeleteBookByIdAsync(id);
        }
    }
}
