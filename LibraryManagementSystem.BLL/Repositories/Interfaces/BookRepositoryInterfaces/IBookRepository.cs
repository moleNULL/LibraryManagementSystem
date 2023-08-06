using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetBooksAsync();
        Task<BookEntity?> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookEntity bookEntity);
        Task<bool> UpdateBookAsync(BookEntity bookEntity);
        Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds);
        Task<bool> DeleteBookByIdAsync(int id);
    }
}
