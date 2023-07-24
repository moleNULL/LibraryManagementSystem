namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class PublisherEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
