using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<PublisherEntity>
    {
        public void Configure(EntityTypeBuilder<PublisherEntity> builder)
        {
            builder.ToTable("Publishers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(100);
        }
    }
}
