using LibraryManagementSystem.BLL.Comparers;
using LibraryManagementSystem.BLL.Exceptions;
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
            return await _dbContext.Books
                .Include(b => b.BookGenres)
                .Include(b => b.Description)
                .Include(b => b.Warehouse)
                .ToListAsync();
        }

        public async Task<BookEntity?> GetBookByIdAsync(int id)
        {
            var bookEntity = await _dbContext.Books.Include(b => b.BookGenres).FirstOrDefaultAsync(b => b.Id == id);
            return bookEntity;
        }

        public async Task<int> AddBookAsync(BookEntity bookEntity)
        {
            _dbContext.Books.Add(bookEntity);
            await _dbContext.SaveChangesAsync();

            // code below is needed to correctly assign BookId for DescriptionEntity and WarehouseEntity
            if (bookEntity.Description is not null)
            {
                var addedDescription =
                    await _dbContext.Descriptions.FirstOrDefaultAsync(description =>
                        description.Description == bookEntity.Description.Description);
            
                if (addedDescription is not null)
                {
                    addedDescription.BookId = bookEntity.Id;
                    _dbContext.Update(addedDescription);
                }   
            }

            var addedWarehouse =
                await _dbContext.Warehouse.OrderByDescending(w => w.Id).FirstOrDefaultAsync();
            if (addedWarehouse is not null)
            {
                addedWarehouse.BookId = bookEntity.Id;
                _dbContext.Update(addedWarehouse);
            }
            await _dbContext.SaveChangesAsync();
            

            return bookEntity.Id;
        }

        public async Task<bool> UpdateBookAsync(BookEntity bookEntity)
        {
            var existingBookEntity = await _dbContext.Books
                .Include(b => b.BookGenres)
                .Include(b => b.Description)
                .Include(b => b.Warehouse)
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
                    existingBookEntity.Description = bookEntity.Description;
                    existingBookEntity.Warehouse = bookEntity.Warehouse;
                    existingBookEntity.LanguageId = bookEntity.LanguageId;
                    existingBookEntity.BookLoanId = bookEntity.BookLoanId;
                    
                    if (!Enumerable.SequenceEqual(
                            bookEntity.BookGenres, existingBookEntity.BookGenres, new BookGenreEntityEqualityComparer()))
                    {
                        _dbContext.BookGenres.RemoveRange(existingBookEntity.BookGenres);
                        existingBookEntity.BookGenres = bookEntity.BookGenres;
                    }
                    
                    _dbContext.Books.Update(existingBookEntity);
                    int countUpdated = await _dbContext.SaveChangesAsync();

                    return countUpdated > 0;
                }

                return true;
            }

            throw new NotFoundException($"There is no book with Id: {bookEntity.Id}");
        }

        public async Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds)
        {
            /* var booksToDelete =
                await _dbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();*/

            bool areAnyDeleted = false;
            foreach (var id in bookIds)
            {
                bool result = await DeleteBookByIdAsync(id);
                areAnyDeleted |= result; // if any book is deleted return true
            }

            return areAnyDeleted;
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToDelete is not null)
            {
                _dbContext.Books.Remove(bookToDelete);
                int countDeleted = await _dbContext.SaveChangesAsync();

                return countDeleted > 0;
            }

            return false;
        }
    }
}
