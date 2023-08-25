namespace LibraryManagementSystem.BLL.Models.Dtos.BookDtos
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
    }
}