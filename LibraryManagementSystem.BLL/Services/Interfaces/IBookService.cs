using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(BookDto bookDto);
        Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds);
        Task<bool> DeleteBookByIdAsync(int id);
    }
}
