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
            return await _dbContext.Books.Select(bookEntity => MapToBookDataModel(bookEntity)).ToListAsync();
        }

        public async Task<BookDataModel?> GetBookByIdAsync(int id)
        {
            var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookEntity is not null)
            {
                var bookDataModel = MapToBookDataModel(bookEntity);
                return bookDataModel;
            }

            return null;
        }

        public async Task AddBookAsync(BookDataModel bookDataModel)
        {
            var bookEntity = MapToBookEntity(bookDataModel);

            _dbContext.Books.Add(bookEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDataModel bookDataModel)
        {
            var bookEntity = MapToBookEntity(bookDataModel);

            var existingBookEntity = await _dbContext.Books
                .Include(b => b.BookGenres)
                .FirstAsync(book => book.Id == bookDataModel.Id);

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
            /*var booksToDelete =
                await _dbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();*/

            foreach (var id in bookIds)
            {
                await DeleteBookByIdAsync(id);
            }
        }

        public async Task DeleteBookByIdAsync(int id)
        {
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToDelete is not null)
            {
                _dbContext.Books.Remove(bookToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        private BookEntity MapToBookEntity(BookDataModel bookDataModel)
        {
            var bookEntity = new BookEntity
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

            // need to use 'Book' (not BookId) to work correctly with both AddBookEntity() and UpdateBookEntity()
            bookEntity.BookGenres = bookDataModel.GenreIds.Select(i => new BookGenreEntity
            {
                Book = bookEntity,
                GenreId = i,
            }).ToList();

            return bookEntity;
        }

        // Without static it could potentially cause a memory leak; static won't capture constant in the instance.
        private static BookDataModel MapToBookDataModel(BookEntity bookEntity)
        {
            var bookDataModel = new BookDataModel
            {
                Id = bookEntity.Id,
                Title = bookEntity.Title,
                PictureName = bookEntity.PictureName,
                PagesNumber = bookEntity.PagesNumber,
                Year = bookEntity.Year,

                PublisherId = bookEntity.PublisherId,
                AuthorId = bookEntity.AuthorId,
                DescriptionId = bookEntity.DescriptionId,
                WarehouseId = bookEntity.WarehouseId,
                LanguageId = bookEntity.LanguageId,
                GenreIds = bookEntity.BookGenres.Select(bg => bg.GenreId),
                BookLoanId = bookEntity.BookLoanId
            };

            return bookDataModel;
        }
    }
}
