namespace LibraryManagementSystem.IntegrationTests.DbUtilities.FakeData
{
    public static class BooksEntityFakeDataGenerator
    {
        private const int MIN_PAGES = 200;
        private const int MAX_PAGES = 1300;
        private const int MIN_YEAR = 1920;
        private const int MAX_YEAR = 2023;
        private const int MIN_PUBLISHER_ID = 1;
        private const int MAX_PUBLISHER_ID = 5;
        private const int MIN_AUTHOR_ID = 1;
        private const int MAX_AUTHOR_ID = 8;
        private const int MIN_LANGUAGE_ID = 1;
        private const int MAX_LANGUAGE_ID = 4;
        private const int MIN_GENRE_ID = 1;
        private const int MAX_GENRE_ID = 11;
        private const int MIN_QUANTITY = 4;
        private const int MAX_QUANTITY = 15;
    
        public static IEnumerable<BookEntity> GenerateBooksEntity(int number)
        {
            return GenerateFakerBookEntity().Generate(number);
        }
    
        private static Faker<BookEntity> GenerateFakerBookEntity()
        {
            return new Faker<BookEntity>()
                .RuleFor(b => b.Id, f => f.IndexFaker + 1)
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(f.Random.Int(1, 5)))
                .RuleFor(b => b.PictureName, f => f.Internet.Avatar())
                .RuleFor(b => b.PagesNumber, f => f.Random.Int(MIN_PAGES, MAX_PAGES))
                .RuleFor(b => b.Year, f => f.Random.Int(MIN_YEAR, MAX_YEAR))
                .RuleFor(b => b.PublisherId, f => f.Random.Int(MIN_PUBLISHER_ID, MAX_PUBLISHER_ID))
                .RuleFor(b => b.AuthorId, f => f.Random.Int(MIN_AUTHOR_ID, MAX_AUTHOR_ID))
                .RuleFor(b => b.DescriptionId, f => f.IndexFaker + 1)
                .RuleFor(b => b.WarehouseId, f => f.IndexFaker + 1)
                .RuleFor(b => b.LanguageId, f => f.Random.Int(MIN_LANGUAGE_ID, MAX_LANGUAGE_ID))
                .RuleFor(b => b.BookLoanId, _ => null)
                .RuleFor(b => b.Description, f => 
                    GenerateFakerDescriptionEntity(f.IndexFaker + 1))
                .RuleFor(b => b.Warehouse, f => 
                    GenerateFakerWarehouseEntity(f.IndexFaker + 1))
                .RuleFor(b => b.BookGenres, f => 
                    GenerateFakerBookGenresEntity(f.IndexFaker + 1).Generate(1)); // genreId shouldn't repeat
        }

        private static Faker<DescriptionEntity> GenerateFakerDescriptionEntity(int index)
        {
            return new Faker<DescriptionEntity>()
                .RuleFor(d => d.Id, _ => index)
                .RuleFor(d => d.BookId, _ => index)
                .RuleFor(d => d.Description, f => 
                    f.Lorem.Sentences(f.Random.Int(5, 20)));
        }

        private static Faker<WarehouseEntity> GenerateFakerWarehouseEntity(int index)
        {
            return new Faker<WarehouseEntity>()
                .RuleFor(w => w.Id, _ => index)
                .RuleFor(w => w.BookId, _ => index)
                .RuleFor(w => w.Price, f => 
                    Math.Round((f.Random.Decimal(0.0499m, 0.1m) * 100), 2))
                .RuleFor(w => w.Quantity, f => f.Random.Int(MIN_QUANTITY, MAX_QUANTITY));
        }

        private static Faker<BookGenreEntity> GenerateFakerBookGenresEntity(int index)
        {
            return new Faker<BookGenreEntity>()
                .RuleFor(bg => bg.BookId, _ => index)
                .RuleFor(bg => bg.GenreId, f => f.Random.Int(MIN_GENRE_ID, MAX_GENRE_ID));
        }
    }
}