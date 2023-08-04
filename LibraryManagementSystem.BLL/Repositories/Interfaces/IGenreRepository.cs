using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces;

public interface IGenreRepository
{
    Task<IEnumerable<GenreEntity>> GetGenresAsync();
    Task<GenreEntity?> GetGenreByIdAsync(int id);
    Task<int> AddGenreAsync(GenreEntity genreEntity);
    Task<bool> UpdateGenreAsync(GenreEntity genreEntity);
    Task<bool> DeleteGenresAsync(IEnumerable<int> genreIds);
    Task<bool> DeleteGenreByIdAsync(int id);
}