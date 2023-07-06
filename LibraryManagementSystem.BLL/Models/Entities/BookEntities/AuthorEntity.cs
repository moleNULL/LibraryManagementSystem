namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class AuthorEntity
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public ICollection<BookEntity> Books { get; init; } = new List<BookEntity>();
    }
}
