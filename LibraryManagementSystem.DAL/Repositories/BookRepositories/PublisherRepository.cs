using LibraryManagementSystem.BLL.Comparers.BookComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories.BookRepositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public PublisherRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<PublisherEntity>> GetPublishersAsync()
    {
        return await _dbContext.Publishers.ToListAsync();
    }

    public async Task<PublisherEntity?> GetPublisherByIdAsync(int id)
    {
        return await _dbContext.Publishers.FirstOrDefaultAsync(publisher => publisher.Id == id);
    }

    public async Task<int> AddPublisherAsync(PublisherEntity publisherEntity)
    {
        _dbContext.Publishers.Add(publisherEntity);
        await _dbContext.SaveChangesAsync();

        return publisherEntity.Id;
    }

    public async Task<bool> UpdatePublisherAsync(PublisherEntity publisherEntity)
    {
        var existingPublisherEntity = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == publisherEntity.Id);

        if (existingPublisherEntity is not null)
        {
            var publisherComparer = new PublisherEntityEqualityComparer();
            if (!publisherComparer.Equals(existingPublisherEntity, publisherEntity))
            {
                existingPublisherEntity.Name = publisherEntity.Name;

                _dbContext.Publishers.Update(existingPublisherEntity);
                int countUpdated = await _dbContext.SaveChangesAsync();

                return countUpdated > 0;
            }

            return true;
        }

        throw new NotFoundException($"There is no publisher with Id: {publisherEntity.Id}");
    }

    public async Task<bool> DeletePublishersAsync(IEnumerable<int> publisherIds)
    {
        bool areAnyDeleted = false;
        foreach (int id in publisherIds)
        {
            bool result = await DeletePublisherByIdAsync(id);
            areAnyDeleted |= result; // if any publisher is deleted return true
        }

        return areAnyDeleted;
    }

    public async Task<bool> DeletePublisherByIdAsync(int id)
    {
        var publisherToDelete = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == id);
        
        if (publisherToDelete is not null)
        {
            _dbContext.Publishers.Remove(publisherToDelete);
            int countDeleted = await _dbContext.SaveChangesAsync();

            return countDeleted > 0;
        }

        return false;
    }
}