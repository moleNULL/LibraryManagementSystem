using LibraryManagementSystem.BLL.Comparers;
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

        public async Task<IEnumerable<BookEntity>> GetBooksAsync()
        {
            return await _dbContext.Books.Include(b => b.BookGenres).ToListAsync();
        }

        public async Task<BookEntity?> GetBookByIdAsync(int id)
        {
            var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            return bookEntity;
        }

        public async Task AddBookAsync(BookEntity bookEntity)
        {
            _dbContext.Books.Add(bookEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookEntity bookEntity)
        {
            var existingBookEntity = await _dbContext.Books
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(book => book.Id == bookEntity.Id);

            var bookEntityComparer = new BookEntityEqualityComparer();

            if (existingBookEntity is not null)
            {
                if (!bookEntityComparer.Equals(existingBookEntity, bookEntity))
                {
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
            }
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
    }
}
