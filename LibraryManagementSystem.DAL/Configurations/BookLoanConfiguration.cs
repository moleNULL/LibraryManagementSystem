using LibraryManagementSystem.BLL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations
{
    public class BookLoanConfiguration : IEntityTypeConfiguration<BookLoanEntity>
    {
        public void Configure(EntityTypeBuilder<BookLoanEntity> builder)
        {
            builder.ToTable("BookLoans");

            builder.HasKey(bl => bl.Id);

            builder.HasOne(bl => bl.Book)
                .WithOne(b => b.BookLoan)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bl => bl.Student)
                .WithMany(s => s.BookLoans)
                .HasForeignKey(bl => bl.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bl => bl.Librarian)
                .WithMany(l => l.BookLoans)
                .HasForeignKey(bl => bl.LibrarianId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
