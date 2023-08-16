using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions;

public static class StudentSeedExtensions
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(GetPreconfiguredStudents());
        modelBuilder.Entity<StudentGenreEntity>().HasData(GetPreconfiguredStudentGenres());
        modelBuilder.Entity<CityEntity>().HasData(GetPreconfiguredCities());
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
                EntryDate = new DateTime(2014, 09, 01)
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
                EntryDate = new DateTime(2016, 09, 01)
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
                EntryDate = new DateTime(2019, 09, 01)
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

    private static IEnumerable<CityEntity> GetPreconfiguredCities()
    {
        return new List<CityEntity>()
        {
            new CityEntity() { Id = 1, Name = "Kyiv" },
            new CityEntity() { Id = 2, Name = "Kharkiv" },
            new CityEntity() { Id = 3, Name = "Poltava" },
            new CityEntity() { Id = 4, Name = "Lviv" },
            new CityEntity() { Id = 5, Name = "Dnipro" },
            
            new CityEntity() { Id = 6, Name = "Uzhgorod" },
            new CityEntity() { Id = 7, Name = "Ivano-Frankivsk" },
            new CityEntity() { Id = 8, Name = "Zaporizhzhia" },
            new CityEntity() { Id = 9, Name = "Kherson" },
            new CityEntity() { Id = 10, Name = "Sumy" },
            
            new CityEntity() { Id = 11, Name = "Chernihiv" },
        };
    }
}