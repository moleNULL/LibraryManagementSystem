using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Tests.FakeData;

public static class BooksFakeDataGenerator
{
    public static Faker<BookEntity> GenerateFakerBookEntity()
    {
        var bookFaker = new Faker<BookEntity>()
            .CustomInstantiator(f => new BookEntity()
            {
                Id = f.IndexFaker + 1,
                Title = f.Lorem.Sentence(f.Random.Int(1, 5)),
                PictureName = f.Internet.Avatar(),
                PagesNumber = f.Random.Int(200, 1300),
                Year = f.Random.Int(1920, 2023),

                PublisherId = f.Random.Int(1, 5),
                AuthorId = f.Random.Int(1, 8),
                DescriptionId = f.IndexFaker + 1,
                WarehouseId = f.IndexFaker + 1,
                LanguageId = f.Random.Int(1, 4),
                BookLoanId = null
            })
            .RuleFor(b => b.Description, f => new DescriptionEntity()
            {
                Id = f.IndexFaker + 1,
                BookId = f.IndexFaker + 1,
                Description = f.Lorem.Sentences(f.Random.Int(5, 20))
            })
            .RuleFor(b => b.Warehouse, f => new WarehouseEntity()
            {
                Id = f.IndexFaker + 1,
                BookId = f.IndexFaker + 1,
                Price = f.Random.Decimal(0.0499m, 0.1m) * 100,
                Quantity = f.Random.Int(4, 15)
            })
            .RuleFor(b => b.BookGenres, f => new Faker<BookGenreEntity>()
                .CustomInstantiator(_ => new BookGenreEntity()
                {
                    BookId = f.IndexFaker + 1,
                    GenreId = f.Random.Int(1, 11)
                })
            .Generate(f.Random.Int(1, 4)));

        return bookFaker;
    }
    public static IEnumerable<BookEntity> GenerateBooksEntity(int number)
    {
        return GenerateFakerBookEntity().Generate(number);
    }
    
    public static BookDto GenerateBookDto()
    {
        var bookFaker = new Faker<BookDto>()
            .CustomInstantiator(f => new BookDto()
            {
                Id = f.IndexFaker + 1,
                Title = f.Lorem.Sentence(f.Random.Int(1, 5)),
                PictureName = f.Internet.Avatar(),
                PagesNumber = f.Random.Int(200, 1300),
                Year = f.Random.Int(1920, 2023),

                PublisherId = f.Random.Int(1, 5),
                AuthorId = f.Random.Int(1, 8),
                Description = null,
                Warehouse = null!,
                LanguageId = f.Random.Int(1, 4),
                BookLoanId = null,
            })
            .RuleFor(b => b.Warehouse, f => new WarehouseDto()
            {
                Id = f.IndexFaker + 1,
                BookId = f.IndexFaker + 1,
                Price = f.Random.Decimal(0.0499m, 0.1m) * 100,
                Quantity = f.Random.Int(4, 15)
            });

        return bookFaker.Generate();
    }
}