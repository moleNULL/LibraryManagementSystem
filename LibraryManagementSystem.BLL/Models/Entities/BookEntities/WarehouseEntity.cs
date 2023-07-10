namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
