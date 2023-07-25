using LibraryManagementSystem.BLL.Models.DataModels;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDataModel>> GetBooksAsync();
        Task<BookDataModel?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDataModel book);
        Task UpdateBookAsync(BookDataModel books);
        Task DeleteBooksAsync(IEnumerable<int> bookIds);
        Task DeleteBookByIdAsync(int id);
    }
}
