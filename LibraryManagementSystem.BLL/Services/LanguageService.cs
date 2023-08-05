using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces;
using LibraryManagementSystem.BLL.Services.Interfaces;

namespace LibraryManagementSystem.BLL.Services;

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
        if (id < 1)
        {
            throw new ArgumentException("LanguageId cannot be negative or zero");
        }
        
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
        if (languageDto.Id < 1)
        {
            throw new ArgumentException("LanguageId cannot be negative or zero");
        }
            
        var languageEntity = _mapper.Map<LanguageEntity>(languageDto);
        return await _languageRepository.UpdateLanguageAsync(languageEntity);
    }

    public async Task<bool> DeleteLanguageAsync(IEnumerable<int> languageIds)
    {
        if (languageIds.Any(languageId => languageId < 1))
        {
            throw new ArgumentException("LanguageId cannot be negative or zero");
        }
        
        return await _languageRepository.DeleteLanguagesAsync(languageIds);
    }

    public async Task<bool> DeleteLanguageByIdAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("LanguageId cannot be negative or zero");
        }

        return await _languageRepository.DeleteLanguageByIdAsync(id);
    }
}