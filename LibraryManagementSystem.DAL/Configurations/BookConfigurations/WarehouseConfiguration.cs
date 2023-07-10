using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<WarehouseEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseEntity> builder)
        {
            builder.ToTable("Warehouse");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Price).HasColumnType("money");
        }
    }
}
