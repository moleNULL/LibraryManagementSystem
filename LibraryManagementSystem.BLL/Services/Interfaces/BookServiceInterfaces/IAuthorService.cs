using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<int> AddAuthorAsync(AuthorDto authorDto);
    Task<bool> UpdateAuthorAsync(AuthorDto authorDto);
    Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds);
    Task<bool> DeleteAuthorByIdAsync(int id);
}