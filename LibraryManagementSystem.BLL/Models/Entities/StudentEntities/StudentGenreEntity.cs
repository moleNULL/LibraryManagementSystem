using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Models.Entities.StudentEntities
{
    public class StudentGenreEntity
    {
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; } = null!;

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; } = null!;
    }
}