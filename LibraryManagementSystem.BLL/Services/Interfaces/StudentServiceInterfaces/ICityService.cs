using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCitiesAsync();
        Task<CityDto?> GetCityByIdAsync(int id);
        Task<int> AddCityAsync(CityDto cityDto);
        Task<bool> UpdateCityAsync(CityDto cityDto);
        Task<bool> DeleteCitiesAsync(IEnumerable<int> cityIds);
        Task<bool> DeleteCityByIdAsync(int id);
    }
}