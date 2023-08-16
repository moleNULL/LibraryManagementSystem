using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetStudentsAsync();
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<StudentDto?> GetStudentByEmailAsync(string email);
    Task<int> AddStudentAsync(StudentDto studentDto);
    Task<bool> UpdateStudentAsync(StudentDto studentDto);
    Task<bool> DeleteStudentsAsync(IEnumerable<int> bookIds);
    Task<bool> DeleteStudentByIdAsync(int id);
}