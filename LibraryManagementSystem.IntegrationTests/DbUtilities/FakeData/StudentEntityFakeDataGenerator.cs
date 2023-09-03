namespace LibraryManagementSystem.IntegrationTests.DbUtilities.FakeData
{
    public static class StudentEntityFakeDataGenerator
    {
        private const int MIN_YEARS_BEFORE = 1;
        private const int MAX_YEARS_BEFORE = 10;
        private const int MIN_GENRE_ID = 1;
        private const int MAX_GENRE_ID = 11;
        private const int MIN_CITY_ID = 1;
        private const int MAX_CITY_ID = 11;

        public static IEnumerable<StudentEntity> GenerateStudentsEntity(int number)
        {
            return GenerateFakerStudentEntity().Generate(number);
        }

        private static Faker<StudentEntity> GenerateFakerStudentEntity()
        {
            return new Faker<StudentEntity>()
                .RuleFor(s => s.Id, f => f.IndexFaker + 1)
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.StudentGenres, f =>
                    GenerateFakerStudentGenresEntity(f.IndexFaker + 1).Generate(1))
                .RuleFor(s => s.PictureName, _ => null)
                .RuleFor(s => s.CityId, f =>
                    f.Random.Int(MIN_CITY_ID, MAX_CITY_ID))
                .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                .RuleFor(s => s.EntryDate, f =>
                    f.Date.Past(f.Random.Int(MIN_YEARS_BEFORE, MAX_YEARS_BEFORE)));
        }

        private static Faker<StudentGenreEntity> GenerateFakerStudentGenresEntity(int index)
        {
            return new Faker<StudentGenreEntity>()
                .RuleFor(sg => sg.StudentId, _ => index)
                .RuleFor(sg => sg.GenreId, f => f.Random.Int(MIN_GENRE_ID, MAX_GENRE_ID));
        }
    }
}