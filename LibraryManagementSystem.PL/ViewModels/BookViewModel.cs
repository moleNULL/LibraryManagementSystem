namespace LibraryManagementSystem.PL.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PictureName { get; set; }
        public int PagesNumber { get; set; }
        public int Year { get; set; }

        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public int? DescriptionId { get; set; }
        public int? WarehouseId { get; set; }
        public int LanguageId { get; set; }
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();
        public int? BookLoanId { get; set; }
    }
}
