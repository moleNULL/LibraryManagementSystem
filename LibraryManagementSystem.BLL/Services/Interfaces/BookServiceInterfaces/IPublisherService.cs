using LibraryManagementSystem.BLL.Models.Dtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

public interface IPublisherService
{
    Task<IEnumerable<PublisherDto>> GetPublishersAsync();
    Task<PublisherDto?> GetPublisherByIdAsync(int id);
    Task<int> AddPublisherAsync(PublisherDto publisherDto);
    Task<bool> UpdatePublisherAsync(PublisherDto publisherDto);
    Task<bool> DeletePublishersAsync(IEnumerable<int> publisherIds);
    Task<bool> DeletePublisherByIdAsync(int id);
}