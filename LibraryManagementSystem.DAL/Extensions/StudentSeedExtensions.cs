using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions;

public static class StudentSeedExtensions
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(GetPreconfiguredStudents());
        modelBuilder.Entity<StudentGenreEntity>().HasData(GetPreconfiguredStudentGenres());
    }

    private static IEnumerable<StudentEntity> GetPreconfiguredStudents()
    {
        return new List<StudentEntity>()
        {
            new StudentEntity()
            {
                Id = 1,
                FirstName = "Christopher",
                LastName = "Anderson",
                Email = "christopher.anderson.test@gmail.com",
                PictureName = "christopher_anderson.png",
                CityId = null,
                Address = "123 Taras Shevchenko Street, Kyiv",
                EntryDate = DateTime.Now
            },
            new StudentEntity()
            {
                Id = 2,
                FirstName = "John",
                LastName = "Mitchell",
                Email = "john.mitchell.library@gmail.com",
                PictureName = "john_mitchell.png",
                CityId = null,
                Address = "56 Petro Sahaidachny Street, Poltava",
                EntryDate = DateTime.Now - TimeSpan.FromDays(1)
            },
            new StudentEntity()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Williams",
                Email = "michael.williams.library@gmail.com",
                PictureName = "michael_williams.png",
                CityId = null,
                Address = "89 Lesya Ukrainka, Kharkiv",
                EntryDate = DateTime.Now - TimeSpan.FromDays(2)
            }
        };
    }

    private static IEnumerable<StudentGenreEntity> GetPreconfiguredStudentGenres()
    {
        return new List<StudentGenreEntity>()
        {
            new StudentGenreEntity() { StudentId = 1, GenreId = 2 },
            new StudentGenreEntity() { StudentId = 1, GenreId = 8 },
            new StudentGenreEntity() { StudentId = 1, GenreId = 10 },
            
            new StudentGenreEntity() { StudentId = 2, GenreId = 5 },
            new StudentGenreEntity() { StudentId = 2, GenreId = 9 },
            new StudentGenreEntity() { StudentId = 2, GenreId = 10 },
            
            new StudentGenreEntity() { StudentId = 3, GenreId = 3 },
            new StudentGenreEntity() { StudentId = 3, GenreId = 6 },
            new StudentGenreEntity() { StudentId = 3, GenreId = 11 },
        };
    }
}