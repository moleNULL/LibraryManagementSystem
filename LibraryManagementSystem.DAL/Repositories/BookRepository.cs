using LibraryManagementSystem.BLL.Comparers;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookDataModel>> GetBooksAsync()
        {
            return await _dbContext.Books.Select(book => new BookDataModel
            {
                Id = book.Id,
                Title = book.Title,
                PictureName = book.PictureName,
                PagesNumber = book.PagesNumber,
                Year = book.Year,

                PublisherId = book.PublisherId,
                AuthorId = book.AuthorId,
                DescriptionId = book.DescriptionId,
                WarehouseId = book.WarehouseId,
                LanguageId = book.LanguageId,
                GenreIds = book.BookGenres.Select(bg => bg.GenreId),
                BookLoanId = book.BookLoanId
            }).ToListAsync();
        }

        public async Task AddBookAsync(BookDataModel bookDataModel)
        {
            var bookEntity = CreateBookEntityFromBookDataModel(bookDataModel);

            _dbContext.Books.Add(bookEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDataModel bookDataModel)
        {
            var bookEntity = CreateBookEntityFromBookDataModel(bookDataModel);

            var existingBookEntity = _dbContext.Books.First(book => book.Id == bookDataModel.Id);
            existingBookEntity.Title = bookEntity.Title;
            existingBookEntity.PictureName = bookEntity.PictureName;
            existingBookEntity.PagesNumber = bookEntity.PagesNumber;
            existingBookEntity.Year = bookEntity.Year;
            existingBookEntity.PublisherId = bookEntity.PublisherId;
            existingBookEntity.AuthorId = bookEntity.AuthorId;
            existingBookEntity.DescriptionId = bookEntity.DescriptionId;
            existingBookEntity.WarehouseId = bookEntity.WarehouseId;
            existingBookEntity.LanguageId = bookEntity.LanguageId;
            existingBookEntity.BookLoanId = bookEntity.BookLoanId;

            if (!Enumerable.SequenceEqual(
                    bookEntity.BookGenres, existingBookEntity.BookGenres, new BookGenreEntityEqualityComparer()))
            {
                _dbContext.BookGenres.RemoveRange(existingBookEntity.BookGenres);
                existingBookEntity.BookGenres = bookEntity.BookGenres;
            }

            _dbContext.Books.Update(existingBookEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            var booksToDelete = await _dbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();

            foreach (var bookToDelete in booksToDelete)
            {
                await DeleteBookAsync(bookToDelete);
            }
        }

        // id
        private async Task DeleteBookAsync(BookEntity book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        private BookEntity CreateBookEntityFromBookDataModel(BookDataModel bookDataModel)
        {
            var bookEntity = new BookEntity()
            {
                Id = bookDataModel.Id,
                Title = bookDataModel.Title,
                PictureName = bookDataModel.PictureName,
                PagesNumber = bookDataModel.PagesNumber,
                Year = bookDataModel.Year,

                PublisherId = bookDataModel.PublisherId,
                AuthorId = bookDataModel.AuthorId,
                DescriptionId = bookDataModel.DescriptionId,
                WarehouseId = bookDataModel.WarehouseId,
                LanguageId = bookDataModel.LanguageId,
                BookLoanId = bookDataModel.BookLoanId
            };

            // need Book and not BookId in order to work both with AddBookEntity and UpdateBookEntity
            bookEntity.BookGenres = bookDataModel.GenreIds.Select(i => new BookGenreEntity
            {
                Book = bookEntity,
                GenreId = i,
            }).ToList();

            return bookEntity;
        }
    }
}
