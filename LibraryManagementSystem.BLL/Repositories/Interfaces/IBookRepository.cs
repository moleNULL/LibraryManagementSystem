using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDataModel>> GetBooksAsync();
        Task<IEnumerable<BookEntity>> GetBooksEntityAsync();
        Task AddBookAsync(BookEntity book);
        Task UpdateBooksAsync(IEnumerable<BookEntity> books);
        Task DeleteBooksAsync(IEnumerable<int> bookIds);
    }
}
