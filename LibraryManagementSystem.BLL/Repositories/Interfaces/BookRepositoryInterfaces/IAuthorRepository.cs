using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorEntity>> GetAuthorsAsync();
        Task<AuthorEntity?> GetAuthorByIdAsync(int id);
        Task<int> AddAuthorAsync(AuthorEntity authorEntity);
        Task<bool> UpdateAuthorAsync(AuthorEntity authorEntity);
        Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds);
        Task<bool> DeleteAuthorByIdAsync(int id);
    }
}