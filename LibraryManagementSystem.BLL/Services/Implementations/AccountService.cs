using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces;

namespace LibraryManagementSystem.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly ILibrarianRepository _librarianRepository;

        public AccountService(
            IMapper mapper, IStudentRepository studentRepository, ILibrarianRepository librarianRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _librarianRepository = librarianRepository;
        }
    
        public async Task<string?> GetUserRoleAsync(string email)
        {
            var librarianEntity = await _librarianRepository.GetLibrarianByEmailAsync(email);
            if (librarianEntity is not null)
            {
                return "librarian";
            }
        
            var studentEntity = await _studentRepository.GetStudentByEmailAsync(email);
            if (studentEntity is not null)
            {
                return "student";
            }

            return null;
        }

        public async Task<bool> RegisterStudentAsync(StudentDto studentDto)
        {
            return await _studentRepository.AddStudentAsync(_mapper.Map<StudentEntity>(studentDto)) > 0;
        }
    }
}