using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<int> AddAuthorAsync(AuthorDto authorDto);
    Task<bool> UpdateAuthorAsync(AuthorDto authorDto);
    Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds);
    Task<bool> DeleteAuthorByIdAsync(int id);
}