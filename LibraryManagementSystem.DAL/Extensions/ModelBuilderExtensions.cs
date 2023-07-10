using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorEntity>().HasData(GetPreconfiguredAuthors());
            modelBuilder.Entity<LanguageEntity>().HasData(GetPreconfiguredLanguages());
            modelBuilder.Entity<PublisherEntity>().HasData(GetPreconfiguredPublishers());
        }

        private static IEnumerable<AuthorEntity> GetPreconfiguredAuthors()
        {
            return new List<AuthorEntity>()
            {
                new AuthorEntity { Id = 1, FirstName = "Ayn", LastName = "Rand" },
                new AuthorEntity { Id = 2, FirstName = "F. Scott", LastName = "Fitzgerald" },
                new AuthorEntity { Id = 3, FirstName = "Jules", LastName = "Verne" },
                new AuthorEntity { Id = 4, FirstName = "Agatha", LastName = "Christie" },
                new AuthorEntity { Id = 5, FirstName = "George", LastName = "Orwell" },

                new AuthorEntity { Id = 6, FirstName = "Stephen", LastName = "King" },
                new AuthorEntity { Id = 7, FirstName = "George", LastName = "Martin" },
                new AuthorEntity { Id = 8, FirstName = "Haruki", LastName = "Murakami" },
            };
        }

        private static IEnumerable<LanguageEntity> GetPreconfiguredLanguages()
        {
            return new List<LanguageEntity>()
            {
                new LanguageEntity { Id = 1, Name = "English" },
                new LanguageEntity { Id = 2, Name = "French" },
                new LanguageEntity { Id = 3, Name = "Japanese" },
                new LanguageEntity { Id = 4, Name = "Russian" },
                new LanguageEntity { Id = 5, Name = "Ukrainian"},
            };
        }

        private static IEnumerable<PublisherEntity> GetPreconfiguredPublishers()
        {
            return new List<PublisherEntity>()
            {
                new PublisherEntity { Id = 1, Name = "AST" },
                new PublisherEntity { Id = 2, Name = "ZNANNIA" },
                new PublisherEntity { Id = 3, Name = "Signet" },
            };
        }
    }
}
