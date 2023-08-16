using LibraryManagementSystem.BLL.Models.Entities;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.DAL.Configurations;
using LibraryManagementSystem.DAL.Configurations.BookConfigurations;
using LibraryManagementSystem.DAL.Configurations.LibrarianConfigurations;
using LibraryManagementSystem.DAL.Configurations.StudentConfigurations;
using LibraryManagementSystem.DAL.Extensions;
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
        public DbSet<BookLoanEntity> BookLoans { get; set; }


        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<DescriptionEntity> Descriptions { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<BookGenreEntity> BookGenres { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<WarehouseEntity> Warehouse { get; set; }

        public DbSet<CityEntity> Cities { get; set; }
        
        public DbSet<StudentGenreEntity> StudentGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyBookConfigurations(modelBuilder);
            ApplyStudentConfigurations(modelBuilder);
            ApplyLibrarianConfigurations(modelBuilder);
            ApplyBookLoanConfigurations(modelBuilder);

            modelBuilder.Seed();
        }

        private void ApplyBookConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new BookGenreConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        }

        private void ApplyStudentConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentGenreConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }

        private void ApplyLibrarianConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibrarianConfiguration());
        }

        private void ApplyBookLoanConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookLoanConfiguration());
        }
    }
}
