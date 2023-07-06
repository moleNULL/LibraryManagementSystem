using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.LibrarianEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Models.Entities
{
    public class BookManager
    {
        public int Id { get; init; }
        public int BookId { get; init; }
        public BookEntity Book { get; init; } = new();
        public int StudentId { get; init; }
        public StudentEntity Student { get; init; } = new();
        public int LibrarianId { get; init; }
        public LibrarianEntity Librarian { get; init; } = new();
        public DateTime IssueDate { get; init; }
        public DateTime ReturnDate { get; init; }
    }
}
