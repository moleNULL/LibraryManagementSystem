using LibraryManagementSystem.BLL.Comparers.StudentComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories.StudentRepositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _dbContext;
    
        public CityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<IEnumerable<CityEntity>> GetCitiesAsync()
        {
            return await _dbContext.Cities.ToListAsync();
        }

        public async Task<CityEntity?> GetCityByIdAsync(int id)
        {
            return await _dbContext.Cities.FirstOrDefaultAsync(city => city.Id == id);
        }

        public async Task<int> AddCityAsync(CityEntity cityEntity)
        {
            _dbContext.Cities.Add(cityEntity);
            await _dbContext.SaveChangesAsync();

            return cityEntity.Id;
        }

        public async Task<bool> UpdateCityAsync(CityEntity cityEntity)
        {
            var existingCityEntity = await _dbContext.Cities
                .FirstOrDefaultAsync(city => city.Id == cityEntity.Id);

            if (existingCityEntity is not null)
            {
                var cityComparer = new CityEntityEqualityComparer();
                if (!cityComparer.Equals(existingCityEntity, cityEntity))
                {
                    existingCityEntity.Name = cityEntity.Name;

                    _dbContext.Cities.Update(existingCityEntity);
                    int countUpdated = await _dbContext.SaveChangesAsync();

                    return countUpdated > 0;
                }

                return true;
            }

            throw new NotFoundException($"There is no librarian with Id: {cityEntity.Id}");
        }

        public async Task<bool> DeleteCitiesAsync(IEnumerable<int> cityIds)
        {
            bool areAnyDeleted = false;
            foreach (int id in cityIds)
            {
                bool result = await DeleteCityByIdAsync(id);
                areAnyDeleted |= result; // if any city is deleted return true
            }

            return areAnyDeleted;
        }

        public async Task<bool> DeleteCityByIdAsync(int id)
        {
            var cityToDelete = await _dbContext.Cities
                .FirstOrDefaultAsync(city => city.Id == id);
        
            if (cityToDelete is not null)
            {
                _dbContext.Cities.Remove(cityToDelete);
                int countDeleted = await _dbContext.SaveChangesAsync();

                return countDeleted > 0;
            }

            return false;
        }
    }
}