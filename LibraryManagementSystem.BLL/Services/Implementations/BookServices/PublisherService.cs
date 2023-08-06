using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices;

public class PublisherService : IPublisherService
{
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    
    public PublisherService(IMapper mapper, IPublisherRepository publisherRepository)
    {
        _mapper = mapper;
        _publisherRepository = publisherRepository;
    }
    
    public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
    {
        var publishersEntity = await _publisherRepository.GetPublishersAsync();
        var publishersDto = _mapper.Map<IEnumerable<PublisherEntity>, IEnumerable<PublisherDto>>(publishersEntity);

        return publishersDto;
    }

    public async Task<PublisherDto?> GetPublisherByIdAsync(int id)
    {
        ValidateId(id);
        
        var publisherEntity = await _publisherRepository.GetPublisherByIdAsync(id);
        if (publisherEntity is not null)
        {
            var publisherDto = _mapper.Map<PublisherEntity, PublisherDto>(publisherEntity);
            return publisherDto;    
        }

        return null;
    }

    public async Task<int> AddPublisherAsync(PublisherDto publisherDto)
    {
        var publisherEntity = _mapper.Map<PublisherEntity>(publisherDto);
        var publishersInDbEntity = await _publisherRepository.GetPublishersAsync();

        if (publishersInDbEntity.All(pid => pid.Name != publisherEntity.Name))
        {
            return await _publisherRepository.AddPublisherAsync(publisherEntity);
        }

        throw new ArgumentException("This publisher already exists");
    }

    public async Task<bool> UpdatePublisherAsync(PublisherDto publisherDto)
    {
        ValidateId(publisherDto.Id);
            
        var publisherEntity = _mapper.Map<PublisherEntity>(publisherDto);
        return await _publisherRepository.UpdatePublisherAsync(publisherEntity);
    }

    public async Task<bool> DeletePublishersAsync(IEnumerable<int> publisherIds)
    {
        foreach (int id in publisherIds)
        {
            ValidateId(id);
        }
        
        return await _publisherRepository.DeletePublishersAsync(publisherIds);
    }

    public async Task<bool> DeletePublisherByIdAsync(int id)
    {
        ValidateId(id);
        
        return await _publisherRepository.DeletePublisherByIdAsync(id);
    }

    private void ValidateId(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("PublisherId cannot be negative or zero");
        }
    }
}