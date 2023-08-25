using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;

namespace LibraryManagementSystem.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<string?> GetUserRoleAsync(string email);
        public Task<bool> RegisterStudentAsync(StudentDto studentDto);
    }
}