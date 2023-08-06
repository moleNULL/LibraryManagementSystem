using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

public interface ILanguageService
{
    Task<IEnumerable<LanguageDto>> GetLanguagesAsync();
    Task<LanguageDto?> GetLanguageByIdAsync(int id);
    Task<int> AddLanguageAsync(LanguageDto languageDto);
    Task<bool> UpdateLanguageAsync(LanguageDto languageDto);
    Task<bool> DeleteLanguageAsync(IEnumerable<int> languageIds);
    Task<bool> DeleteLanguageByIdAsync(int id);
}