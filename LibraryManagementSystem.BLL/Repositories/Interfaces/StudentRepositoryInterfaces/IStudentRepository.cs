using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;

public interface IStudentRepository
{
    Task<IEnumerable<StudentEntity>> GetStudentsAsync();
    Task<StudentEntity?> GetStudentByIdAsync(int id);
    Task<StudentEntity?> GetStudentByEmailAsync(string email);
    Task<int> AddStudentAsync(StudentEntity studentEntity);
    Task<bool> UpdateStudentAsync(StudentEntity studentEntity);
    Task<bool> DeleteStudentsAsync(IEnumerable<int> studentIds);
    Task<bool> DeleteStudentByIdAsync(int id);
}