using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;

namespace LibraryManagementSystem.BLL.Tests.FakeData
{
    public static class StudentFakeDataGenerator
    {
        public static StudentDto GenerateStudentDto()
        {
            var studentDtoFaker = new Faker<StudentDto>()
                .CustomInstantiator(f => new StudentDto()
                {
                    Id = f.Random.Int(1, 3),
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    Email = f.Internet.Email(),
                    FavoriteGenreIds = Enumerable.Range(1, 11),
                    PictureName = null,
                    CityId = f.Random.Int(1, 11),
                    Address = f.Address.StreetAddress(),
                    EntryDate = f.Date.Past(f.Random.Int(1, 10))
                });

            return studentDtoFaker.Generate();
        }
    }
}