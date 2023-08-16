using AutoMapper;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    
    public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    public async Task<IEnumerable<AuthorSimpleDto>> GetAuthorsAsync()
    {
        var authorsEntity = await _authorRepository.GetAuthorsAsync();
        var authorsSimpleDto = _mapper.Map<IEnumerable<AuthorEntity>, IEnumerable<AuthorSimpleDto>>(authorsEntity);

        return authorsSimpleDto;
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);
        
        var authorEntity = await _authorRepository.GetAuthorByIdAsync(id);
        if (authorEntity is not null)
        {
            var authorDto = _mapper.Map<AuthorEntity, AuthorDto>(authorEntity);
            return authorDto;    
        }

        return null;
    }

    public async Task<int> AddAuthorAsync(AuthorDto authorDto)
    {
        var authorEntity = _mapper.Map<AuthorEntity>(authorDto);
        var authorsInDbEntity = await _authorRepository.GetAuthorsAsync();

        if (!authorsInDbEntity.Any(aid => 
                aid.FirstName == authorEntity.FirstName &&
                aid.LastName == authorEntity.LastName))
        {
            return await _authorRepository.AddAuthorAsync(authorEntity);
        }

        throw new ArgumentException("This author already exists");
    }

    public async Task<bool> UpdateAuthorAsync(AuthorDto authorDto)
    {
        ValidationHelper.ValidateId(authorDto.Id);
            
        var authorEntity = _mapper.Map<AuthorEntity>(authorDto);
        return await _authorRepository.UpdateAuthorAsync(authorEntity);
    }

    public async Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds)
    {
        foreach (int id in authorIds)
        {
            ValidationHelper.ValidateId(id);
        }
        
        return await _authorRepository.DeleteAuthorsAsync(authorIds);
    }

    public async Task<bool> DeleteAuthorByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        return await _authorRepository.DeleteAuthorByIdAsync(id);
    }
}