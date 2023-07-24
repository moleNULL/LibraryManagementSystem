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
        public virtual PublisherEntity Publisher { get; set; } = null!;

        public int AuthorId { get; set; }
        public virtual AuthorEntity Author { get; set; } = null!;

        public int? DescriptionId { get; set; }
        public virtual DescriptionEntity? Description { get; set; }

        public int? WarehouseId { get; set; }
        public virtual WarehouseEntity? Warehouse { get; set; } = null!;

        public int LanguageId { get; set; }
        public virtual LanguageEntity Language { get; set; } = null!;

        public virtual ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
        public virtual ICollection<BookGenreEntity> BookGenres { get; set; } = new List<BookGenreEntity>();

        public int? BookLoanId { get; set; }
        public virtual BookLoanEntity? BookLoan { get; set; }
    }
}
