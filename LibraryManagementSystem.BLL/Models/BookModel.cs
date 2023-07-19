﻿namespace LibraryManagementSystem.BLL.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PictureName { get; set; }
        public int PagesNumber { get; set; }
        public int Year { get; set; }

        public string Publisher { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Language { get; set; } = string.Empty;
        public ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public int? BookLoanId { get; set; }
    }
}
