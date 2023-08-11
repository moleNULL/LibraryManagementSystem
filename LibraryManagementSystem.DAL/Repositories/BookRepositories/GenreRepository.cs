using LibraryManagementSystem.BLL.Comparers.BookComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories.BookRepositories;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GenreRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GenreEntity>> GetGenresAsync()
    {
        return await _dbContext.Genres.ToListAsync();
    }

    public async Task<GenreEntity?> GetGenreByIdAsync(int id)
    {
        return await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
    }

    public async Task<int> AddGenreAsync(GenreEntity genreEntity)
    {
        _dbContext.Genres.Add(genreEntity);
        await _dbContext.SaveChangesAsync();

        return genreEntity.Id;
    }

    public async Task<bool> UpdateGenreAsync(GenreEntity genreEntity)
    {
        var existingGenreEntity = 
            await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == genreEntity.Id);

        if (existingGenreEntity is not null)
        {
            var genreComparer = new GenreEntityEqualityComparer();
            if (!genreComparer.Equals(existingGenreEntity, genreEntity))
            {
                existingGenreEntity.Name = genreEntity.Name;

                _dbContext.Genres.Update(existingGenreEntity);
                int countUpdated = await _dbContext.SaveChangesAsync();

                return countUpdated > 0;
            }

            return true;
        }

        throw new NotFoundException($"There is no genre with Id: {genreEntity.Id}");
    }

    public async Task<bool> DeleteGenresAsync(IEnumerable<int> genreIds)
    {
        bool areAnyDeleted = false;
        foreach (var id in genreIds)
        {
            bool result = await DeleteGenreByIdAsync(id);
            areAnyDeleted |= result; // if any genre is deleted return true
        }

        return areAnyDeleted;
    }

    public async Task<bool> DeleteGenreByIdAsync(int id)
    {
        var genreToDelete = await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
        if (genreToDelete is not null)
        {
            _dbContext.Genres.Remove(genreToDelete);
            int countDeleted = await _dbContext.SaveChangesAsync();

            return countDeleted > 0;
        }

        return false;
    }
}