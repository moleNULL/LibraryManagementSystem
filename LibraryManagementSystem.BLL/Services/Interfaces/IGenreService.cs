using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetGenresAsync();
    Task<GenreDto?> GetGenreByIdAsync(int id);
    Task<int> AddGenreAsync(GenreDto genreDto);
    Task<bool> UpdateGenreAsync(GenreDto genreDto);
    Task<bool> DeleteGenresAsync(IEnumerable<int> genreIds);
    Task<bool> DeleteGenreByIdAsync(int id);
}