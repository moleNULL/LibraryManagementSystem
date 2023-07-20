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
            var books = await (from book in _dbContext.Books
                               join publisher in _dbContext.Publishers on book.PublisherId equals publisher.Id
                               join author in _dbContext.Authors on book.AuthorId equals author.Id
                               join description in _dbContext.Descriptions on book.DescriptionId equals description.Id
                               join warehouse in _dbContext.Warehouse on book.WarehouseId equals warehouse.Id
                               join language in _dbContext.Languages on book.LanguageId equals language.Id
                               join bookgenre in _dbContext.BookGenres on book.Id equals bookgenre.BookId
                               join genre in _dbContext.Genres on bookgenre.GenreId equals genre.Id
                               select new BookDataModel
                               {
                                   Id = book.Id,
                                   Title = book.Title,
                                   PictureName = book.PictureName,
                                   PagesNumber = book.PagesNumber,
                                   Year = book.Year,

                                   Publisher = publisher.Name,
                                   Author = author.FirstName + " " + author.LastName,
                                   Description = description.Description,
                                   Price = warehouse.Price,
                                   Quantity = warehouse.Quantity,
                                   Language = language.Name,
                                   Genres = book.BookGenres.Select(bg => bg.Genre.Name),

                                   BookLoanId = null
                               })
                               .GroupBy(b => b.Id) // If a book has multiple genres associated with it, the join
                                                   // operation will create a separate row for each genre, resulting
                                                   // in duplicate book entries.
                               .Select(distinctBook => distinctBook.First())
                               .ToListAsync();

            return books;
        }

        public async Task<IEnumerable<BookEntity>> GetBooksEntityAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task AddBookAsync(BookEntity book)
        {
            if (!await _dbContext.Books.AnyAsync(b => b.Title == book.Title && b.AuthorId == book.AuthorId))
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
            }

            throw new ArgumentException("This book already exists");
        }

        // bookentity
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

        // id
        private async Task DeleteBookAsync(BookEntity book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
