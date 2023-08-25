namespace LibraryManagementSystem.PL.ViewModels.StudentViewModels.StudentViewModels
{
    public class StudentAddViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<int> FavoriteGenreIds { get; set; } = new List<int>();
        // public string? PictureName { get; set; }
        // public int? CityId { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; }
    }
}