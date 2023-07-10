using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title).HasMaxLength(75);
            builder.Property(b => b.PictureName).HasMaxLength(75);

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Description)
                .WithOne(d => d.Book)
                .HasForeignKey<BookEntity>(b => b.DescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Warehouse)
                .WithMany(w => w.Books)
                .HasForeignKey(b => b.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Language)
                .WithMany(l => l.Books)
                .HasForeignKey(b => b.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(j => j.ToTable("BooksGenres"));

            builder.HasOne(b => b.BookLoan)
                .WithOne(bl => bl.Book)
                .HasForeignKey<BookEntity>(b => b.BookLoanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
