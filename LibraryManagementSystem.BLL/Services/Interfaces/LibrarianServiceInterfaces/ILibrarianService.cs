using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.LibrarianServiceInterfaces
{
    public interface ILibrarianService
    {
        Task<IEnumerable<LibrarianDto>> GetLibrariansAsync();
        Task<LibrarianDto?> GetLibrarianByIdAsync(int id);
        Task<LibrarianDto?> GetLibrarianByEmailAsync(string email);
        Task<int> AddLibrarianAsync(LibrarianDto librarianDto);
        Task<bool> UpdateLibrarianAsync(LibrarianDto librarianDto);
        Task<bool> DeleteLibrariansAsync(IEnumerable<int> librarianIds);
        Task<bool> DeleteLibrarianByIdAsync(int id);
    }
}