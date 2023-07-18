using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName).HasMaxLength(100);
            builder.Property(a => a.LastName).HasMaxLength(100);
        }
    }
}
