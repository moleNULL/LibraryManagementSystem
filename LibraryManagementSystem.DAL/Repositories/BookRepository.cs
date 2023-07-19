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
            var books = await (from b in _dbContext.Books
                               join p in _dbContext.Publishers on b.PublisherId equals p.Id
                               join a in _dbContext.Authors on b.AuthorId equals a.Id
                               join d in _dbContext.Descriptions on b.DescriptionId equals d.Id
                               join w in _dbContext.Warehouse on b.WarehouseId equals w.Id
                               join l in _dbContext.Languages on b.LanguageId equals l.Id
                               join bg in _dbContext.BookGenres on b.Id equals bg.BookId
                               join g in _dbContext.Genres on bg.GenreId equals g.Id
                               select new BookDataModel
                               {
                                   Id = b.Id,
                                   Title = b.Title,
                                   PictureName = b.PictureName,
                                   PagesNumber = b.PagesNumber,
                                   Year = b.Year,

                                   Publisher = p.Name,
                                   Author = a.FirstName + " " + a.LastName,
                                   Description = d.Description,
                                   Price = w.Price,
                                   Quantity = w.Quantity,
                                   Language = l.Name,
                                   Genres = b.Genres,
                                   BookLoanId = null
                               }).ToListAsync();

            return books;
        }

        public async Task<IEnumerable<BookEntity>> GetBooksEntityAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task AddBookAsync(BookEntity book)
        {
            if (!_dbContext.Books.Any(b => b.Title == book.Title && b.AuthorId == book.AuthorId))
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();
            }

            throw new ArgumentException("This book already exists");
        }

        public async Task UpdateBooksAsync(IEnumerable<BookEntity> books)
        {
            var booksInDb = await _dbContext.Books.ToListAsync();
            var modifiedBooks = books.Except(booksInDb, new BookEntityEqualityComparer());

            // BLL

            if (modifiedBooks.Any())
            {
                _dbContext.Books.UpdateRange(modifiedBooks);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            var booksToDelete = await _dbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();

            foreach (var bookToDelete in booksToDelete)
            {
                await DeleteBookAsync(bookToDelete);
            }
        }

        private async Task DeleteBookAsync(BookEntity book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
