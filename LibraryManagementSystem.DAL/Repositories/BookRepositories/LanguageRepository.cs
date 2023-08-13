using LibraryManagementSystem.BLL.Comparers.BookComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories.BookRepositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public LanguageRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<LanguageEntity>> GetLanguagesAsync()
    {
        return await _dbContext.Languages.ToListAsync();
    }

    public async Task<LanguageEntity?> GetLanguageByIdAsync(int id)
    {
        return await _dbContext.Languages.FirstOrDefaultAsync(language => language.Id == id);
    }

    public async Task<int> AddLanguageAsync(LanguageEntity languageEntity)
    {
        _dbContext.Languages.Add(languageEntity);
        await _dbContext.SaveChangesAsync();

        return languageEntity.Id;
    }

    public async Task<bool> UpdateLanguageAsync(LanguageEntity languageEntity)
    {
        var existingLanguageEntity = 
            await _dbContext.Languages.FirstOrDefaultAsync(language => language.Id == languageEntity.Id);

        if (existingLanguageEntity is not null)
        {
            var languageComparer = new LanguageEntityEqualityComparer();
            if (!languageComparer.Equals(existingLanguageEntity, languageEntity))
            {
                existingLanguageEntity.Name = languageEntity.Name;

                _dbContext.Languages.Update(existingLanguageEntity);
                int countUpdated = await _dbContext.SaveChangesAsync();

                return countUpdated > 0;
            }

            return true;
        }

        throw new NotFoundException($"There is no language with Id: {languageEntity.Id}");
    }

    public async Task<bool> DeleteLanguagesAsync(IEnumerable<int> languageIds)
    {
        bool areAnyDeleted = false;
        foreach (int id in languageIds)
        {
            bool result = await DeleteLanguageByIdAsync(id);
            areAnyDeleted |= result; // if any language is deleted return true
        }

        return areAnyDeleted;
    }

    public async Task<bool> DeleteLanguageByIdAsync(int id)
    {
        var languageToDelete = 
            await _dbContext.Languages.FirstOrDefaultAsync(language => language.Id == id);
        
        if (languageToDelete is not null)
        {
            _dbContext.Languages.Remove(languageToDelete);
            int countDeleted = await _dbContext.SaveChangesAsync();

            return countDeleted > 0;
        }

        return false;
    }
}