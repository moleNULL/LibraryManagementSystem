using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
        public ICollection<BookGenreEntity> BookGenres { get; set; } = new List<BookGenreEntity>();

        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
        public ICollection<StudentGenreEntity> StudentGenres { get; set; } = new List<StudentGenreEntity>();
    }
}
