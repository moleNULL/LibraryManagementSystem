using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.StudentConfigurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FirstName).HasMaxLength(100);
            builder.Property(s => s.LastName).HasMaxLength(100);
            builder.Property(s => s.PictureName).HasMaxLength(100);

            builder.Property(s => s.Address).HasMaxLength(200);

            builder.HasOne(s => s.City)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
