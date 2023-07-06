namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class DescriptionEntity
    {
        public int Id { get; init; }
        public string Description { get; init; } = string.Empty;
        public int BookId { get; init; }
        public BookEntity Book { get; init; } = new();
    }
}
