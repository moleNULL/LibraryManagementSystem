using System.Text.Json.Serialization;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.WarehouseViewModels;

namespace LibraryManagementSystem.PL.ViewModels.BookViewModels.BookViewModels
{
    public class BookViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        // public string? PictureName { get; set; }
        [JsonPropertyName("pagesNumber")]
        public int PagesNumber { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("publisherId")]
        public int PublisherId { get; set; }
        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("warehouse")]
        public WarehouseViewModel Warehouse { get; set; } = null!;
        [JsonPropertyName("languageId")]
        public int LanguageId { get; set; }
        [JsonPropertyName("genreIds")]
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();
        [JsonPropertyName("bookLoan")]
        public int? BookLoanId { get; set; }
    }
}
