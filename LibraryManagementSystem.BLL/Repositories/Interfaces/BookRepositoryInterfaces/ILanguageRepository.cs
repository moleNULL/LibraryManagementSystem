using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<LanguageEntity>> GetLanguagesAsync();
        Task<LanguageEntity?> GetLanguageByIdAsync(int id);
        Task<int> AddLanguageAsync(LanguageEntity languageEntity);
        Task<bool> UpdateLanguageAsync(LanguageEntity languageEntity);
        Task<bool> DeleteLanguagesAsync(IEnumerable<int> languageIds);
        Task<bool> DeleteLanguageByIdAsync(int id);
    }
}