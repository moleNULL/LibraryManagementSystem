using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetBooksAsync();
        Task<BookEntity?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookEntity book);
        Task UpdateBookAsync(BookEntity books);
        Task DeleteBooksAsync(IEnumerable<int> bookIds);
        Task DeleteBookByIdAsync(int id);
    }
}
