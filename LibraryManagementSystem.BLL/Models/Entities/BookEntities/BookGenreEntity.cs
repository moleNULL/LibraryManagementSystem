namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class BookGenreEntity
    {
        public int BookId { get; set; }
        public BookEntity Book { get; set; } = null!;

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; } = null!;
    }
}
