using LibraryManagementSystem.BLL.Comparers;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories;
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
        public async Task<IEnumerable<BookEntity>> GetBooksAsync()
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
