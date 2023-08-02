using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces;
using LibraryManagementSystem.BLL.Services.Interfaces;

namespace LibraryManagementSystem.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    
    public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var authorsEntity = await _authorRepository.GetAuthorsAsync();
        var authorsDto = _mapper.Map<IEnumerable<AuthorEntity>, IEnumerable<AuthorDto>>(authorsEntity);

        return authorsDto;
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("AuthorId cannot be negative or zero");
        }
        
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

        if (!authorsInDbEntity.Any(
                aid => aid.FirstName == authorEntity.FirstName && aid.LastName == authorEntity.LastName))
        {
            return await _authorRepository.AddAuthorAsync(authorEntity);
        }

        throw new ArgumentException("This author already exists");
    }

    public async Task<bool> UpdateAuthorAsync(AuthorDto authorDto)
    {
        if (authorDto.Id < 1)
        {
            throw new ArgumentException("AuthorId cannot be negative or zero");
        }
            
        var authorEntity = _mapper.Map<AuthorEntity>(authorDto);
        return await _authorRepository.UpdateAuthorAsync(authorEntity);
    }

    public Task<bool> DeleteAuthorsAsync(IEnumerable<int> authorIds)
    {
        if (authorIds.Any(bookId => bookId < 1))
        {
            throw new ArgumentException("AuthorId cannot be negative or zero");
        }
        
        return _authorRepository.DeleteAuthorsAsync(authorIds);
    }

    public async Task<bool> DeleteAuthorByIdAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("AuthorId cannot be negative or zero");
        }

        return await _authorRepository.DeleteAuthorByIdAsync(id);
    }
}