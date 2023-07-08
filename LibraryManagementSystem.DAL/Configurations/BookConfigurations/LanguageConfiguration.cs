using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DAL.Configurations.BookConfigurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<LanguageEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageEntity> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name).HasMaxLength(50);
        }
    }
}
