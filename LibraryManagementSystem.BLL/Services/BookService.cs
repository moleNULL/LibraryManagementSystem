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
            var booksDataModel = await _bookRepository.GetBooksAsync();
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksDataModel);

            return booksDto;
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<BookEntity>(bookDto);
            await _bookRepository.AddBookAsync(bookEntity);
        }

        public async Task UpdateBooksAsync(IEnumerable<BookDto> booksDto)
        {
            var booksInDbDataModel = await _bookRepository.GetBooksAsync();
            //var booksDataModel = _mapper.Map<IEnumerable>
        }

        public async Task DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            await _bookRepository.DeleteBooksAsync(bookIds);
        }
    }
}
