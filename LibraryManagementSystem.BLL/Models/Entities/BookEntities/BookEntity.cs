namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class BookEntity
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public int PagesNumber { get; init; }
        public int PublisherId { get; init; }
        public PublisherEntity Publisher { get; init; } = new();
        public int AuthorId { get; init; }
        public AuthorEntity Author { get; init; } = new();
        public string? PictureName { get; init; }
        public int? DescriptionId { get; init; }
        public DescriptionEntity? Description { get; init; }
        public int GenreId { get; init; }
        public GenreEntity Genre { get; init; } = new();
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public int LanguageId { get; init; }
        public LanguageEntity Language { get; init; } = new();
    }
}
