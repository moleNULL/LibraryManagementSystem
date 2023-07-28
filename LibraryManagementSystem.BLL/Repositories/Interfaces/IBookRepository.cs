using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetBooksAsync();
        Task<BookEntity?> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookEntity book);
        Task<bool> UpdateBookAsync(BookEntity books);
        Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds);
        Task<bool> DeleteBookByIdAsync(int id);
    }
}
