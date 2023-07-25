namespace LibraryManagementSystem.BLL.Models.Entities.StudentEntities
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PictureName { get; set; }
        public int? CityId { get; set; }
        public CityEntity? City { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; }
        public ICollection<BookLoanEntity> BookLoans { get; set; } = new List<BookLoanEntity>();
    }
}
