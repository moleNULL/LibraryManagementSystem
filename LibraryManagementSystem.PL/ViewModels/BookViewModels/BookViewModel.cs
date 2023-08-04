using LibraryManagementSystem.PL.ViewModels.WarehouseViewModels;

namespace LibraryManagementSystem.PL.ViewModels.BookViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        // public string? PictureName { get; set; }
        public int PagesNumber { get; set; }
        public int Year { get; set; }

        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public string? Description { get; set; }
        public WarehouseViewModel Warehouse { get; set; } = null!;
        public int LanguageId { get; set; }
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();
        public int? BookLoanId { get; set; }
    }
}
