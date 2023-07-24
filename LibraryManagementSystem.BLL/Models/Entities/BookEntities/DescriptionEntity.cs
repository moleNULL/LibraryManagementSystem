namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class DescriptionEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int BookId { get; set; }
        public virtual BookEntity Book { get; set; } = null!;
    }
}
