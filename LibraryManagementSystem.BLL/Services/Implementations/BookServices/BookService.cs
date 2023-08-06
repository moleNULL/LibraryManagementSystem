using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices
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
            ValidateId(id);

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

            if (!booksInDbEntity.Any(
                bid => bid.Title == bookEntity.Title && bid.AuthorId == bookEntity.AuthorId))
            {
                return await _bookRepository.AddBookAsync(bookEntity);
            }

            throw new ArgumentException("This book already exists");
        }

        public async Task<bool> UpdateBookAsync(BookDto bookDto)
        {
            ValidateId(bookDto.Id);
            
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            return await _bookRepository.UpdateBookAsync(bookEntity);
        }

        public async Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            foreach (int id in bookIds)
            {
                ValidateId(id);
            }
            
            return await _bookRepository.DeleteBooksAsync(bookIds);
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            ValidateId(id);

            return await _bookRepository.DeleteBookByIdAsync(id);
        }
        
        private void ValidateId(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("BookId cannot be negative or zero");
            }
        }
    }
}
