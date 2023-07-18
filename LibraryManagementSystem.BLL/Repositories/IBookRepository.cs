using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetBooksAsync();
        Task AddBookAsync(BookEntity book);
        Task UpdateBooksAsync(IEnumerable<BookEntity> books);
        Task DeleteBooksAsync(IEnumerable<int> bookIds);
    }
}
