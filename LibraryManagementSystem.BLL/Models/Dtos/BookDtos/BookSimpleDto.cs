namespace LibraryManagementSystem.BLL.Models.Dtos.BookDtos;

public class BookSimpleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Description { get; set; }
    
    public int AuthorId { get; set; }
    public IEnumerable<int> GenreIds { get; set; } = new List<int>();
}