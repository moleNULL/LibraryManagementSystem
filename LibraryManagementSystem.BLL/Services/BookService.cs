using AutoMapper;
using LibraryManagementSystem.BLL.Comparers;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Dtos;
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

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var booksDataModel = await _bookRepository.GetBooksAsync();
            var bookDataModel = booksDataModel.FirstOrDefault(b => b.Id == id);

            var bookDto = _mapper.Map<BookDto>(bookDataModel);

            return bookDto;
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            var bookDataModel = _mapper.Map<BookDataModel>(bookDto);
            var booksInDbDataModel = await _bookRepository.GetBooksAsync();

            if (!booksInDbDataModel.Any(
                bid => bid.Title == bookDataModel.Title && bid.AuthorId == bookDataModel.AuthorId))
            {
                await _bookRepository.AddBookAsync(bookDataModel);
                return;
            }

            throw new ArgumentException("This book already exists");
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var bookDataModel = _mapper.Map<BookDataModel>(bookDto);
            var booksInDbDataModel = await _bookRepository.GetBooksAsync();

            var bookDataModelComparer = new BookDataModelEqualityComparer();

            var existingBookDataModel = booksInDbDataModel.FirstOrDefault(bid => bid.Id == bookDataModel.Id);
            if (existingBookDataModel is not null)
            {
                if (!bookDataModelComparer.Equals(existingBookDataModel, bookDataModel))
                {
                    await _bookRepository.UpdateBookAsync(bookDataModel);
                }
            }
        }

        public async Task DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            await _bookRepository.DeleteBooksAsync(bookIds);
        }
    }
}
