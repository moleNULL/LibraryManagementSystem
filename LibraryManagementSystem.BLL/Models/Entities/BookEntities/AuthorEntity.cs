namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
