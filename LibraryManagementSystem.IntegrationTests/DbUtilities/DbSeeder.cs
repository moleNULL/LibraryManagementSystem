namespace LibraryManagementSystem.IntegrationTests.DbUtilities
{
    public static class DbSeeder
    {
        private const int BOOKS_NUMBER_TO_GENERATE = 50;
        private const int STUDENTS_NUMBER_TO_GENERATE = 5;

        public static IEnumerable<BookEntity> GetPreconfiguredBooks()
        {
            return BooksEntityFakeDataGenerator.GenerateBooksEntity(BOOKS_NUMBER_TO_GENERATE);
        }

        public static IEnumerable<StudentEntity> GetPreconfiguredStudents()
        {
            return StudentEntityFakeDataGenerator.GenerateStudentsEntity(STUDENTS_NUMBER_TO_GENERATE);
        }
    }
}