using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<PublisherEntity>> GetPublishersAsync();
        Task<PublisherEntity?> GetPublisherByIdAsync(int id);
        Task<int> AddPublisherAsync(PublisherEntity publisherEntity);
        Task<bool> UpdatePublisherAsync(PublisherEntity publisherEntity);
        Task<bool> DeletePublishersAsync(IEnumerable<int> publisherIds);
        Task<bool> DeletePublisherByIdAsync(int id);
    }
}