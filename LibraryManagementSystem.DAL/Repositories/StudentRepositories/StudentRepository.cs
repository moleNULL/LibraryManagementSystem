using LibraryManagementSystem.BLL.Comparers.StudentComparers;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Repositories.StudentRepositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public StudentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<StudentEntity>> GetStudentsAsync()
    {
        return await _dbContext.Students
            .Include(s => s.StudentGenres)
            .ToListAsync();
    }

    public async Task<StudentEntity?> GetStudentByIdAsync(int id)
    {
        return await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
    }

    public async Task<int> AddStudentAsync(StudentEntity studentEntity)
    {
        _dbContext.Students.Add(studentEntity);
        await _dbContext.SaveChangesAsync();

        return studentEntity.Id;
    }

    public async Task<bool> UpdateStudentAsync(StudentEntity studentEntity)
    {
        var existingStudentEntity = await _dbContext.Students
            .Include(student => student.StudentGenres)
            .FirstOrDefaultAsync(student => student.Id == studentEntity.Id);

        if (existingStudentEntity is not null)
        {
            var studentComparer = new StudentEntityEqualityComparer();
            if (!studentComparer.Equals(existingStudentEntity, studentEntity))
            {
                existingStudentEntity.FirstName = studentEntity.FirstName;
                existingStudentEntity.LastName = studentEntity.LastName;
                existingStudentEntity.Email = studentEntity.Email;
                existingStudentEntity.PictureName = studentEntity.PictureName;
                existingStudentEntity.CityId = studentEntity.CityId;
                existingStudentEntity.Address = studentEntity.Address;
                existingStudentEntity.EntryDate = studentEntity.EntryDate;

                if (!Enumerable.SequenceEqual(
                        studentEntity.StudentGenres, existingStudentEntity.StudentGenres, new StudentGenreEntityEqualityComparer()))
                {
                    _dbContext.StudentGenres.RemoveRange(existingStudentEntity.StudentGenres);
                    existingStudentEntity.StudentGenres = studentEntity.StudentGenres;
                }
                
                _dbContext.Students.Update(existingStudentEntity);
                int countUpdated = await _dbContext.SaveChangesAsync();

                return countUpdated > 0;
            }

            return true;
        }

        throw new NotFoundException($"There is no student with Id: {studentEntity.Id}");
    }

    public async Task<bool> DeleteStudentsAsync(IEnumerable<int> studentIds)
    {
        bool areAnyDeleted = false;
        foreach (var id in studentIds)
        {
            bool result = await DeleteStudentByIdAsync(id);
            areAnyDeleted |= result; // if any student is deleted return true
        }

        return areAnyDeleted;
    }

    public async Task<bool> DeleteStudentByIdAsync(int id)
    {
        var studentToDelete = 
            await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
        
        if (studentToDelete is not null)
        {
            _dbContext.Students.Remove(studentToDelete);
            int countDeleted = await _dbContext.SaveChangesAsync();

            return countDeleted > 0;
        }

        return false;
    }
}