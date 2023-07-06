namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class LanguageEntity
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public ICollection<BookEntity> Books { get; init; } = new List<BookEntity>();
    }
}
