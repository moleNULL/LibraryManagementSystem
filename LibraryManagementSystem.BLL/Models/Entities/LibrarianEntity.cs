namespace LibraryManagementSystem.BLL.Models.Entities
{
    public class LibrarianEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PictureName { get; set; }
        public DateTime EntryDate { get; set; }
        public virtual ICollection<BookLoanEntity> BookLoans { get; set; } = new List<BookLoanEntity>();
    }
}
