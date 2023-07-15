using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenreEntity>
    {
        public void Configure(EntityTypeBuilder<BookGenreEntity> builder)
        {
            builder.ToTable("BookGenres");

            builder.HasKey(bg => new { bg.BookId, bg.GenreId });

            builder.HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
