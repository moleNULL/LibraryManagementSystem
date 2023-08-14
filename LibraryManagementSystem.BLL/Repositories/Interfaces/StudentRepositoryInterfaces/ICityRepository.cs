using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;

public interface ICityRepository
{
    Task<IEnumerable<CityEntity>> GetCitiesAsync();
    Task<CityEntity?> GetCityByIdAsync(int id);
    Task<int> AddCityAsync(CityEntity cityEntity);
    Task<bool> UpdateCityAsync(CityEntity cityEntity);
    Task<bool> DeleteCitiesAsync(IEnumerable<int> cityIds);
    Task<bool> DeleteCityByIdAsync(int id);
}