using LibraryManagementSystem.BLL.Models.Entities;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.LibrarianEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.DAL.Configurations.BookConfigurations;
using LibraryManagementSystem.DAL.Configurations.LibrarianConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<LibrarianEntity> Librarians { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<BookManager> BookManagers { get; set; }


        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<DescriptionEntity> Descriptions { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            modelBuilder.ApplyConfiguration(new LibrarianConfiguration());
        }
    }
}
