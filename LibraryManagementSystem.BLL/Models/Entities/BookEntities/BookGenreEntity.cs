namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class BookGenreEntity
    {
        public int BookId { get; set; }
        public virtual BookEntity Book { get; set; } = null!;

        public int GenreId { get; set; }
        public virtual GenreEntity Genre { get; set; } = null!;
    }
}
