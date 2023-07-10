using LibraryManagementSystem.BLL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.LibrarianConfigurations
{
    public class LibrarianConfiguration : IEntityTypeConfiguration<LibrarianEntity>
    {
        public void Configure(EntityTypeBuilder<LibrarianEntity> builder)
        {
            builder.ToTable("Librarians");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.FirstName).HasMaxLength(75);
            builder.Property(l => l.LastName).HasMaxLength(75);
            builder.Property(l => l.Email).HasMaxLength(75);
            builder.Property(l => l.PictureName).HasMaxLength(75);
        }
    }
}