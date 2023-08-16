using AutoMapper;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.StudentServices;

public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public CityService(IMapper mapper, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
    }
    
    public async Task<IEnumerable<CityDto>> GetCitiesAsync()
    {
        var citiesEntity = await _cityRepository.GetCitiesAsync();
        var citiesDto = 
            _mapper.Map<IEnumerable<CityEntity>, IEnumerable<CityDto>>(citiesEntity);

        return citiesDto;
    }

    public async Task<CityDto?> GetCityByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);
        
        var cityEntity = await _cityRepository.GetCityByIdAsync(id);
        if (cityEntity is not null)
        {
            var cityDto = _mapper.Map<CityEntity, CityDto>(cityEntity);
            return cityDto;    
        }

        return null;
    }

    public async Task<int> AddCityAsync(CityDto cityDto)
    {
        var cityEntity = _mapper.Map<CityEntity>(cityDto);
        var citiesInDbEntity = await _cityRepository.GetCitiesAsync();

        if (citiesInDbEntity.All(cid => cid.Name != cityEntity.Name))
        {
            return await _cityRepository.AddCityAsync(cityEntity);
        }

        throw new ArgumentException("This city already exists");
    }

    public async Task<bool> UpdateCityAsync(CityDto cityDto)
    {
        ValidationHelper.ValidateId(cityDto.Id);
            
        var cityEntity = _mapper.Map<CityEntity>(cityDto);
        return await _cityRepository.UpdateCityAsync(cityEntity);
    }

    public async Task<bool> DeleteCitiesAsync(IEnumerable<int> cityIds)
    {
        foreach (int id in cityIds)
        {
            ValidationHelper.ValidateId(id);
        }
        
        return await _cityRepository.DeleteCitiesAsync(cityIds);
    }

    public async Task<bool> DeleteCityByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        return await _cityRepository.DeleteCityByIdAsync(id);        
    }
}