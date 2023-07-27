namespace LibraryManagementSystem.PL.Models.ViewModels.BookViewModels
{
    public class BookAddViewModel
    {
        public string Title { get; set; } = string.Empty;
        //public string? PictureName { get; set; }
        public int PagesNumber { get; set; }
        public int Year { get; set; }

        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public int LanguageId { get; set; }
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();
    }
}
