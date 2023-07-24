namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
        public virtual ICollection<BookGenreEntity> BookGenres { get; set; } = new List<BookGenreEntity>();
    }
}
