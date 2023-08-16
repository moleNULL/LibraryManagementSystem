using System.Collections;
using AutoMapper;
using LibraryManagementSystem.BLL.Helpers;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations.StudentServices;

public class StudentService : IStudentService
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;

    public StudentService(IMapper mapper, IStudentRepository studentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }
    
    public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
    {
        var studentsEntity = await _studentRepository.GetStudentsAsync();
        var studentsDto = 
            _mapper.Map<IEnumerable<StudentEntity>, IEnumerable<StudentDto>>(studentsEntity);

        return studentsDto;
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);
        
        var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
        if (studentEntity is not null)
        {
            var studentDto = _mapper.Map<StudentEntity, StudentDto>(studentEntity);
            return studentDto;    
        }

        return null;
    }

    public async Task<StudentDto?> GetStudentByEmailAsync(string email)
    {
        var studentEntity = await _studentRepository.GetStudentByEmailAsync(email);
        if (studentEntity is not null)
        {
            var studentDto = _mapper.Map<StudentEntity, StudentDto>(studentEntity);
            return studentDto;    
        }

        return null;
    }

    public async Task<int> AddStudentAsync(StudentDto studentDto)
    {
        var studentEntity = _mapper.Map<StudentEntity>(studentDto);
        var studentsInDbEntity = await _studentRepository.GetStudentsAsync();

        if (studentsInDbEntity.All(sid => sid.Email != studentEntity.Email))
        {
            return await _studentRepository.AddStudentAsync(studentEntity);
        }

        throw new ArgumentException("This student already exists");
    }

    public async Task<bool> UpdateStudentAsync(StudentDto studentDto)
    {
        ValidationHelper.ValidateId(studentDto.Id);
            
        var studentEntity = _mapper.Map<StudentEntity>(studentDto);
        return await _studentRepository.UpdateStudentAsync(studentEntity);
    }

    public async Task<bool> DeleteStudentsAsync(IEnumerable<int> bookIds)
    {
        foreach (int id in bookIds)
        {
            ValidationHelper.ValidateId(id);
        }
        
        return await _studentRepository.DeleteStudentsAsync(bookIds);
    }

    public async Task<bool> DeleteStudentByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        return await _studentRepository.DeleteStudentByIdAsync(id);
    }
}