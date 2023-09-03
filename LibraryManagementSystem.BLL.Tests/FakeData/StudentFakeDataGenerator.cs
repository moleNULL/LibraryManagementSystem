using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;

namespace LibraryManagementSystem.BLL.Tests.FakeData
{
    public static class StudentFakeDataGenerator
    {
        private const int MIN_YEARS_BEFORE = 1;
        private const int MAX_YEARS_BEFORE = 10;
        private const int MIN_CITY_ID = 1;
        private const int MAX_CITY_ID = 11;
        private const int MAX_GENRE_ID = 11;

        public static StudentDto GenerateStudentDto()
        {
            var studentDtoFaker = new Faker<StudentDto>()
                .RuleFor(s => s.Id, f => f.IndexFaker + 1)
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.PictureName, _ => null)
                .RuleFor(s => s.FavoriteGenreIds, f =>
                    Enumerable.Range(1, f.Random.Int(MAX_GENRE_ID)))
                .RuleFor(s => s.CityId,
                    f => f.Random.Int(MIN_CITY_ID, MAX_CITY_ID))
                .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                .RuleFor(s => s.EntryDate, f =>
                    f.Date.Past(f.Random.Int(MIN_YEARS_BEFORE, MAX_YEARS_BEFORE)));

            return studentDtoFaker.Generate();
        }
    }
}