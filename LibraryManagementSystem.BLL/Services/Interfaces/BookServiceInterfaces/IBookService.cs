using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookSimpleDto>> GetBooksAsync();
        Task<IEnumerable<BookSimpleDto>> GetBooksFilteredByRoleAsync(string email);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(BookDto bookDto);
        Task<bool> DeleteBooksAsync(IEnumerable<int> bookIds);
        Task<bool> DeleteBookByIdAsync(int id);
    }
}
