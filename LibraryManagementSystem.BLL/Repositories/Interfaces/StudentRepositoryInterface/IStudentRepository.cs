using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterface;

public interface IStudentRepository
{
    Task<IEnumerable<StudentEntity>> GetStudentsAsync();
    Task<StudentEntity?> GetStudentByIdAsync(int id);
    Task<int> AddStudentAsync(StudentEntity studentEntity);
    Task<bool> UpdateStudentAsync(StudentEntity studentEntity);
    Task<bool> DeleteStudentsAsync(IEnumerable<int> studentIds);
    Task<bool> DeleteStudentByIdAsync(int id);
}