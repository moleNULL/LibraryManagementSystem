using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.StudentConfigurations;

public class StudentGenreConfiguration : IEntityTypeConfiguration<StudentGenreEntity>
{
    public void Configure(EntityTypeBuilder<StudentGenreEntity> builder)
    {
        builder.ToTable("StudentGenres");

        builder.HasKey(sg => new { sg.StudentId, sg.GenreId });

        builder.HasOne(sg => sg.Student)
            .WithMany(s => s.StudentGenres)
            .HasForeignKey(sg => sg.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sg => sg.Genre)
            .WithMany(g => g.StudentGenres)
            .HasForeignKey(sg => sg.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}