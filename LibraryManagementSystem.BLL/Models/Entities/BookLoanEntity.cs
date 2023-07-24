using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Models.Entities
{
    public class BookLoanEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual BookEntity Book { get; set; } = new();
        public int StudentId { get; set; }
        public virtual StudentEntity Student { get; set; } = new();  // ICollection?
        public int LibrarianId { get; set; }
        public virtual LibrarianEntity Librarian { get; set; } = new();
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
