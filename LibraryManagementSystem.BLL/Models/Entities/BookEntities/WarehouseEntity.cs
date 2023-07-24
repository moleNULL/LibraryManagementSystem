﻿namespace LibraryManagementSystem.BLL.Models.Entities.BookEntities
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
        public virtual BookEntity Book { get; set; } = null!;
    }
}
