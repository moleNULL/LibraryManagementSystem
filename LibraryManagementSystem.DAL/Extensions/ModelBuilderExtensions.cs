using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>().HasData(GetPreconfiguredBooks());
            modelBuilder.Entity<AuthorEntity>().HasData(GetPreconfiguredAuthors());
            modelBuilder.Entity<DescriptionEntity>().HasData(GetPreconfiguredDescriptions());
            modelBuilder.Entity<GenreEntity>().HasData(GetPreconfiguredGenres());
            modelBuilder.Entity<LanguageEntity>().HasData(GetPreconfiguredLanguages());
            modelBuilder.Entity<PublisherEntity>().HasData(GetPreconfiguredPublishers());
            modelBuilder.Entity<WarehouseEntity>().HasData(GetPreconfiguredWarehouse());
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
                new PublisherEntity { Id = 4, Name = "Grapevine" },
            };
        }

        private static IEnumerable<DescriptionEntity> GetPreconfiguredDescriptions()
        {
            return new List<DescriptionEntity>()
            {
                new DescriptionEntity { Id = 1, Description = "We the Living is not a story of politics, " +
                "but of the men and women who have to struggle for existence behind the Red banners and slogans. " +
                "It is a picture of what those slogans do to human beings. What happens to the defiant ones? " +
                "What happens to those who succumb?\r\n\r\nAgainst a vivid panorama of political revolution and " +
                "personal revolt, Ayn Rand shows what the theory of socialism means in practice. " },

                new DescriptionEntity { Id = 2, Description = "Who is John Galt? When he says that he will stop " +
                "the motor of the world, is he a destroyer or a liberator? Why does he have to fight his battles " +
                "not against his enemies but against those who need him most? Why does he fight his hardest " +
                "battle against the woman he loves?\r\n\r\nYou will know the answer to these questions when " +
                "you discover the reason behind the baffling events that play havoc with the lives of the " +
                "amazing men and women in this book. You will discover why a productive genius becomes " +
                "a worthless playboy...why a great steel industrialist is working for his own destruction..." +
                "why a composer gives up his career on the night of his triumph...why a beautiful woman who " +
                "runs a transcontinental railroad falls in love with the man she has sworn to kill." },

                new DescriptionEntity { Id = 3, Description = "\"I don't want to repeat my innocence. I want " +
                "the pleasure of losing it again.\"\r\nYoung Amory Blaine, who is convinced he has an " +
                "exceptionally promising future, finishes boarding school and attends Princeton. At university, " +
                "Amory is an indifferent student, preferring instead to fall in and out of love, and cheerfully " +
                "immersing himself in a glitzy world of excessive drinking and casual liaisons.\r\nWritten at " +
                "the tender age of twenty-three, F. Scott Fitzgerald's first novel mirrors some of his own " +
                "experiences at Princeton University. This romance of the early Jazz Age is a commentary on " +
                "how love can be affected by money and social status." },
            };
        }

        private static IEnumerable<GenreEntity> GetPreconfiguredGenres()
        {
            return new List<GenreEntity>()
            {
                new GenreEntity { Id = 1,  Name = "Bildungsroman" },
                new GenreEntity { Id = 2,  Name = "Philosophical fiction" },
                new GenreEntity { Id = 3,  Name = "Libertarian science fiction" },
                new GenreEntity { Id = 4,  Name = "Mystery fiction" },
                new GenreEntity { Id = 5,  Name = "Romance novel" },

                new GenreEntity { Id = 6,  Name = "Historical fiction" },
                new GenreEntity { Id = 7,  Name = "Semi-autobiographic" },
                new GenreEntity { Id = 8,  Name = "Tragedy" },
                new GenreEntity { Id = 9,  Name = "Science fiction" },
                new GenreEntity { Id = 10, Name = "Adventure novel" },
            };
        }

        private static IEnumerable<WarehouseEntity> GetPreconfiguredWarehouse()
        {
            return new List<WarehouseEntity>()
            {
                new WarehouseEntity { Id = 1, Quantity = 16, Price = 4.49m },
                new WarehouseEntity { Id = 2, Quantity = 10, Price = 24.11m },
                new WarehouseEntity { Id = 3, Quantity = 2, Price = 15.04m },
                new WarehouseEntity { Id = 4, Quantity = 6, Price = 11.61m },
                new WarehouseEntity { Id = 5, Quantity = 23, Price = 12.10m },
            };
        }

        private static IEnumerable<BookEntity> GetPreconfiguredBooks()
        {
            return new List<BookEntity>()
            {
                new BookEntity 
                { 
                    Id = 1, 
                    Title = "We the Living", 
                    PictureName = "ayn_rand_we_the_living.png",
                    PagesNumber = 528,

                    PublisherId = 3,
                    AuthorId = 1,
                    DescriptionId = 1,
                    WarehouseId = 1,
                    LanguageId = 4,
                    Genres = new List<GenreEntity>(),
                    BookLoanId = 1,
                }
            };
        }
    }
}
