using AutoMapper;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.BookServices;

public class GenreService : IGenreService
{
    private readonly IMapper _mapper;
    private readonly IGenreRepository _genreRepository;
    
    public GenreService(IMapper mapper, IGenreRepository genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }
    
    public async Task<IEnumerable<GenreDto>> GetGenresAsync()
    {
        var genresEntity = await _genreRepository.GetGenresAsync();
        var genresDto = _mapper.Map<IEnumerable<GenreEntity>, IEnumerable<GenreDto>>(genresEntity);

        return genresDto;
    }

    public async Task<GenreDto?> GetGenreByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);
        
        var genreEntity = await _genreRepository.GetGenreByIdAsync(id);
        if (genreEntity is not null)
        {
            var genreDto = _mapper.Map<GenreEntity, GenreDto>(genreEntity);
            return genreDto;    
        }

        return null;
    }

    public async Task<int> AddGenreAsync(GenreDto genreDto)
    {
        var genreEntity = _mapper.Map<GenreEntity>(genreDto);
        var genresInDbEntity = await _genreRepository.GetGenresAsync();

        if (genresInDbEntity.All(gid => gid.Name != genreEntity.Name))
        {
            return await _genreRepository.AddGenreAsync(genreEntity);
        }

        throw new ArgumentException("This genre already exists");
    }

    public async Task<bool> UpdateGenreAsync(GenreDto genreDto)
    {
        ValidationHelper.ValidateId(genreDto.Id);
            
        var genreEntity = _mapper.Map<GenreEntity>(genreDto);
        return await _genreRepository.UpdateGenreAsync(genreEntity);
    }

    public async Task<bool> DeleteGenresAsync(IEnumerable<int> genreIds)
    {
        foreach (int id in genreIds)
        {
            ValidationHelper.ValidateId(id);
        }
        
        return await _genreRepository.DeleteGenresAsync(genreIds);
    }

    public async Task<bool> DeleteGenreByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        return await _genreRepository.DeleteGenreByIdAsync(id);
    }
}