namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PictureName { get; set; }
        public int PagesNumber { get; set; }
        public int Year { get; set; }

        public int PublisherId { get; set; }
        public PublisherEntity Publisher { get; set; } = null!;

        public int AuthorId { get; set; }
        public AuthorEntity Author { get; set; } = null!;

        public int? DescriptionId { get; set; }
        public DescriptionEntity? Description { get; set; }

        public int? WarehouseId { get; set; }
        public WarehouseEntity? Warehouse { get; set; } = null!;

        public int LanguageId { get; set; }
        public LanguageEntity Language { get; set; } = null!;

        public ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
        public ICollection<BookGenreEntity> BookGenres { get; set; } = new List<BookGenreEntity>();
        public int? BookLoanId { get; set; }
        public BookLoanEntity? BookLoan { get; set; }
    }
}
