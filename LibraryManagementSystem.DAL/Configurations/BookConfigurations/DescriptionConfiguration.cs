using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class DescriptionConfiguration : IEntityTypeConfiguration<DescriptionEntity>
    {
        public void Configure(EntityTypeBuilder<DescriptionEntity> builder)
        {
            builder.ToTable("Descriptions");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Description).HasColumnType("varchar(max)");
        }
    }
}
