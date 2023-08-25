using LibraryManagementSystem.BLL.Models.Entities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<LibrarianEntity>> GetLibrariansAsync();
        Task<LibrarianEntity?> GetLibrarianByIdAsync(int id);
        Task<LibrarianEntity?> GetLibrarianByEmailAsync(string email);
        Task<int> AddLibrarianAsync(LibrarianEntity librarianEntity);
        Task<bool> UpdateLibrarianAsync(LibrarianEntity librarianEntity);
        Task<bool> DeleteLibrariansAsync(IEnumerable<int> librarianIds);
        Task<bool> DeleteLibrarianByIdAsync(int id);
    }
}