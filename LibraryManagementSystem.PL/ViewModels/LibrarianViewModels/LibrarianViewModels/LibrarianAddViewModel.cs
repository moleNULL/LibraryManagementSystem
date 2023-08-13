namespace LibraryManagementSystem.PL.ViewModels.LibrarianViewModels.LibrarianViewModels;

public class LibrarianAddViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PictureName { get; set; }
    public DateTime EntryDate { get; set; }
}