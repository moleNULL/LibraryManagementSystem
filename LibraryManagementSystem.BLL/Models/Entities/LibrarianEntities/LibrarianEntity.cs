namespace LibraryManagementSystem.BLL.Models.Entities.LibrarianEntities
{
    public class LibrarianEntity
    {
        public int Id { get; init; }
        public string Login { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string? PictureName { get; init; }
        public DateTime EntryDate { get; init; }
    }
}
