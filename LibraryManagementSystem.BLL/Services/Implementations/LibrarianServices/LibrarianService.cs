using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.LibrarianServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.LibrarianServices;

public class LibrarianService : ILibrarianService
{
    private readonly IMapper _mapper;
    private readonly ILibrarianRepository _librarianRepository;

    public LibrarianService(IMapper mapper, ILibrarianRepository librarianRepository)
    {
        _mapper = mapper;
        _librarianRepository = librarianRepository;
    }
    
    public async Task<IEnumerable<LibrarianDto>> GetLibrariansAsync()
    {
        var librariansEntity = await _librarianRepository.GetLibrariansAsync();
        var librariansDto = 
            _mapper.Map<IEnumerable<LibrarianEntity>, IEnumerable<LibrarianDto>>(librariansEntity);

        return librariansDto;
    }

    public async Task<LibrarianDto?> GetLibrarianByIdAsync(int id)
    {
        ValidateId(id);
        
        var librarianEntity = await _librarianRepository.GetLibrarianByIdAsync(id);
        if (librarianEntity is not null)
        {
            var librarianDto = _mapper.Map<LibrarianEntity, LibrarianDto>(librarianEntity);
            return librarianDto;    
        }

        return null;
    }

    public async Task<int> AddLibrarianAsync(LibrarianDto librarianDto)
    {
        var librarianEntity = _mapper.Map<LibrarianEntity>(librarianDto);
        var librariansInDbEntity = await _librarianRepository.GetLibrariansAsync();

        if (librariansInDbEntity.All(lid => lid.Email != librarianEntity.Email))
        {
            return await _librarianRepository.AddLibrarianAsync(librarianEntity);
        }

        throw new ArgumentException("This librarian already exists");
    }

    public async Task<bool> UpdateLibrarianAsync(LibrarianDto librarianDto)
    {
        ValidateId(librarianDto.Id);
            
        var librarianEntity = _mapper.Map<LibrarianEntity>(librarianDto);
        return await _librarianRepository.UpdateLibrarianAsync(librarianEntity);
    }

    public async Task<bool> DeleteLibrariansAsync(IEnumerable<int> librarianIds)
    {
        foreach (int id in librarianIds)
        {
            ValidateId(id);
        }
        
        return await _librarianRepository.DeleteLibrariansAsync(librarianIds);
    }

    public async Task<bool> DeleteLibrarianByIdAsync(int id)
    {
        ValidateId(id);

        return await _librarianRepository.DeleteLibrarianByIdAsync(id);
    }
    
    private void ValidateId(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("LibrarianId cannot be negative or zero");
        }
    }
}