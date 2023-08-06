using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices;

public class LanguageService : ILanguageService
{
    private readonly IMapper _mapper;
    private readonly ILanguageRepository _languageRepository;
    
    public LanguageService(IMapper mapper, ILanguageRepository languageRepository)
    {
        _mapper = mapper;
        _languageRepository = languageRepository;
    }
    
    public async Task<IEnumerable<LanguageDto>> GetLanguagesAsync()
    {
        var languagesEntity = await _languageRepository.GetLanguagesAsync();
        var languagesDto = _mapper.Map<IEnumerable<LanguageEntity>, IEnumerable<LanguageDto>>(languagesEntity);

        return languagesDto;
    }

    public async Task<LanguageDto?> GetLanguageByIdAsync(int id)
    {
        ValidateId(id);
        
        var languageEntity = await _languageRepository.GetLanguageByIdAsync(id);
        if (languageEntity is not null)
        {
            var languageDto = _mapper.Map<LanguageEntity, LanguageDto>(languageEntity);
            return languageDto;    
        }

        return null;
    }

    public async Task<int> AddLanguageAsync(LanguageDto languageDto)
    {
        var languageEntity = _mapper.Map<LanguageEntity>(languageDto);
        var languagesInDbEntity = await _languageRepository.GetLanguagesAsync();

        if (languagesInDbEntity.All(lid => lid.Name != languageEntity.Name))
        {
            return await _languageRepository.AddLanguageAsync(languageEntity);
        }

        throw new ArgumentException("This language already exists");
    }

    public async Task<bool> UpdateLanguageAsync(LanguageDto languageDto)
    {
        ValidateId(languageDto.Id);
            
        var languageEntity = _mapper.Map<LanguageEntity>(languageDto);
        return await _languageRepository.UpdateLanguageAsync(languageEntity);
    }

    public async Task<bool> DeleteLanguageAsync(IEnumerable<int> languageIds)
    {
        foreach (int id in languageIds)
        {
            ValidateId(id);
        }
        
        return await _languageRepository.DeleteLanguagesAsync(languageIds);
    }

    public async Task<bool> DeleteLanguageByIdAsync(int id)
    {
        ValidateId(id);

        return await _languageRepository.DeleteLanguageByIdAsync(id);
    }
    
    private void ValidateId(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("LanguageId cannot be negative or zero");
        }
    }
}