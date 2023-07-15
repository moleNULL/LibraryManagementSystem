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
            modelBuilder.Entity<BookGenreEntity>().HasData(GetPreconfiguredBookGenres());
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
                new LanguageEntity { Id = 4, Name = "Ukrainian"},
            };
        }

        private static IEnumerable<PublisherEntity> GetPreconfiguredPublishers()
        {
            return new List<PublisherEntity>()
            {
                new PublisherEntity { Id = 1, Name = "AST" },
                new PublisherEntity { Id = 2, Name = "ZNANNIA" },
                new PublisherEntity { Id = 3, Name = "Signet" },
                new PublisherEntity { Id = 4, Name = "Fingerprint! Publishing" },
                new PublisherEntity { Id = 5, Name = "Dutton" },
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

                new DescriptionEntity { Id = 4, Description = "\"The Great Gatsby\" is a classic novel written " +
                "by F. Scott Fitzgerald. Set in the lavish and glamorous world of 1920s America, the story follows" +
                "Jay Gatsby, a mysterious and enigmatic millionaire, through the eyes of Nick Carraway, a young " +
                "Midwesterner who becomes Gatsby's neighbor. The book delves into themes of love, wealth, " +
                "decadence, and the illusion of the American Dream." },

                new DescriptionEntity { Id = 5, Description = "When an unidentified monster threatens international" +
                " shipping, French oceanographer Pierre Aronnax and his unflappable assistant Conseil join an " +
                "expedition organized by the US Navy to hunt down and destroy the menace. After months of " +
                "fruitless searching, they finally grapple with their quarry, but Aronnax, Conseil, and the " +
                "brash Canadian harpooner Ned Land are thrown overboard in the attack, only to find that the " +
                "“monster” is actually a futuristic submarine, the Nautilus, commanded by a shadowy, mystical, " +
                "preternaturally imposing man who calls himself Captain Nemo. Thus begins a journey of 20,000 " +
                "leagues—nearly 50,000 miles—that will take Captain Nemo, his crew, and these three adventurers " +
                "on a journey of discovery through undersea forests, coral graveyards, miles-deep trenches, and " +
                "even the sunken ruins of Atlantis. " },


                new DescriptionEntity { Id = 6, Description = "A stolen hot air balloon lands an unlikely crew on " +
                "a mysterious island far from everything they know in this action-packed Jules Verne classic—now" +
                "with an arresting new look!\r\n\r\nFive prisoners of war steal a hot air balloon and escape " +
                "capture in Virginia during the American Civil War. They fly for several thousand miles before " +
                "a storm forces them to crash land on an unknown island in the Pacific. There, the marooned men " +
                "must work together to pool all their knowledge and skill if they wish to survive.\r\n\r\nBut the " +
                "island has its secrets, and the castaways discover it’s not as deserted as they thought. A " +
                "mysterious figure has been watching them. Does it bring salvation or even more danger?" },

                new DescriptionEntity { Id = 7, Description = "The Mighty Orinoco tells the story of a young " +
                "man's search for his father along the then-uncharted Orinoco River of Venezuela. The text " +
                "contains all the ingredients of a classic Verne scientific-adventure tale: exploration and " +
                "discovery, humor and drama, dastardly villains and intrepid heroes, and a host of " +
                "near-fatal encounters with crocodiles, jungle fever, Indians and outlaws ― all set in " +
                "a wonderfully exotic locale. The Mighty Orinoco also includes a unique twist that will " +
                "appeal to feminists ― readers will need to discover it for themselves." },

                new DescriptionEntity { Id = 8, Description = "Just after midnight, a snowdrift stops the famous " +
                "Orient Express in its tracks as it travels through the mountainous Balkans. The luxurious train " +
                "is surprisingly full for the time of the year but, by the morning, it is one passenger fewer. " +
                "An American tycoon lies dead in his compartment, stabbed a dozen times, his door locked from the " +
                "inside.\r\n\r\nOne of the passengers is none other than detective Hercule Poirot. " +
                "On vacation.\r\n\r\nIsolated and with a killer on board, Poirot must identify the murderer—in " +
                "case he or she decides to strike again." },

                new DescriptionEntity { Id = 9, Description = "First, there were ten—a curious assortment of " +
                "strangers summoned as weekend guests to a little private island off the coast of Devon. " +
                "Their host, an eccentric millionaire unknown to all of them, is nowhere to be found. All " +
                "that the guests have in common is a wicked past they're unwilling to reveal—and a secret " +
                "that will seal their fate. For each has been marked for murder. A famous nursery rhyme is " +
                "framed and hung in every room of the mansion.\n\nWhen they realize that murders are occurring " +
                "as described in the rhyme, terror mounts. One by one they fall prey. Before the weekend is out, " +
                "there will be none. Who has choreographed this dastardly scheme? And who will be left to tell " +
                "the tale? Only the dead are above suspicion." },

                new DescriptionEntity { Id = 10, Description = "It’s seven in the morning. The Bantrys wake to " +
                "find the body of a young woman in their library. She is wearing evening dress and heavy make-up, " +
                "which is now smeared across her cheeks.\r\n\r\nBut who is she? How did she get there? And what " +
                "is the connection with another dead girl, whose charred remains are later discovered in an " +
                "abandoned quarry?\r\n\r\nThe respectable Bantrys invite Miss Marple to solve the mystery… " +
                "before tongues start to wag." },


                new DescriptionEntity { Id = 11, Description = "The new novel by George Orwell is the major work " +
                "towards which all his previous writing has pointed. Critics have hailed it as his \"most solid, " +
                "most brilliant\" work. Though the story of Nineteen Eighty-Four takes place thirty-five years " +
                "hence, it is in every sense timely. The scene is London, where there has been no new housing " +
                "since 1950 and where the city-wide slums are called Victory Mansions. Science has abandoned Man " +
                "for the State. As every citizen knows only too well, war is peace." },

                new DescriptionEntity { Id = 12, Description = "A farm is taken over by its overworked, mistreated " +
                "animals. With flaming idealism and stirring slogans, they set out to create a paradise of " +
                "progress, justice, and equality. Thus the stage is set for one of the most telling satiric " +
                "fables ever penned –a razor-edged fairy tale for grown-ups that records the evolution from " +
                "revolution against tyranny to a totalitarianism just as terrible." },

                new DescriptionEntity { Id = 13, Description = "Paul Sheldon. He's a bestselling novelist who " +
                "has finally met his biggest fan. Her name is Annie Wilkes and she is more than a rabid reader - " +
                "she is Paul's nurse, tending his shattered body after an automobile accident. But she is also " +
                "his captor, keeping him prisoner in her isolated house." },

                new DescriptionEntity { Id = 14, Description = "Jack Torrance's new job at the Overlook Hotel " +
                "is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old " +
                "hotel, he'll have plenty of time to spend reconnecting with his family and working on his " +
                "writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote..." +
                "and more sinister. And the only one to notice the strange and terrible forces gathering around " +
                "the Overlook is Danny Torrance, a uniquely gifted five-year-old." },

                new DescriptionEntity { Id = 15, Description = "Long ago, in a time forgotten, a preternatural " +
                "event threw the seasons out of balance. In a land where summers can last decades and winters " +
                "a lifetime, trouble is brewing. The cold is returning, and in the frozen wastes to the north " +
                "of Winterfell, sinister forces are massing beyond the kingdom’s protective Wall. To the south, " +
                "the king’s powers are failing—his most trusted adviser dead under mysterious circumstances and " +
                "his enemies emerging from the shadows of the throne. At the center of the conflict lie the " +
                "Starks of Winterfell, a family as harsh and unyielding as the frozen land they were born to. " +
                "Now Lord Eddard Stark is reluctantly summoned to serve as the king’s new Hand, an appointment " +
                "that threatens to sunder not only his family but the kingdom itself." },


                new DescriptionEntity { Id = 16, Description = "A comet the color of blood and flame cuts " +
                "across the sky. Two great leaders—Lord Eddard Stark and Robert Baratheon—who hold sway over " +
                "an age of enforced peace are dead, victims of royal treachery. Now, from the ancient citadel " +
                "of Dragonstone to the forbidding shores of Winterfell, chaos reigns. Six factions struggle " +
                "for control of a divided land and the Iron Throne of the Seven Kingdoms, preparing to stake " +
                "their claims through tempest, turmoil, and war.\r\n\r\nIt is a tale in which brother plots " +
                "against brother and the dead rise to walk in the night. Here a princess masquerades as an " +
                "orphan boy; a knight of the mind prepares a poison for a treacherous sorceress; and wild men " +
                "descend from the Mountains of the Moon to ravage the countryside. Against a backdrop of incest " +
                "and fratricide, alchemy and murder, victory may go to the men and women possessed of the coldest " +
                "steel...and the coldest hearts. For when kings clash, the whole land trembles." },

                new DescriptionEntity { Id = 17, Description = "Of the five contenders for power, one is dead, " +
                "another in disfavor, and still the wars rage as alliances are made and broken. Joffrey sits on " +
                "the Iron Throne, the uneasy ruler of the Seven Kingdoms. His most bitter rival, Lord Stannis, " +
                "stands defeated and disgraced, victim of the sorceress who holds him in her thrall. Young Robb " +
                "still rules the North from the fortress of Riverrun. Meanwhile, making her way across a " +
                "blood-drenched continent is the exiled queen, Daenerys, mistress of the only three dragons " +
                "still left in the world. And as opposing forces manoeuver for the final showdown, an army of " +
                "barbaric wildlings arrives from the outermost limits of civilization, accompanied by a horde of " +
                "mythical Others—a supernatural army of the living dead whose animated corpses are unstoppable. " +
                "As the future of the land hangs in the balance, no one will rest until the Seven Kingdoms have " +
                "exploded in a veritable storm of swords..." },

                new DescriptionEntity { Id = 18, Description = "Toru, a quiet and preternaturally serious young " +
                "college student in Tokyo, is devoted to Naoko, a beautiful and introspective young woman, but " +
                "their mutual passion is marked by the tragic death of their best friend years before. Toru " +
                "begins to adapt to campus life and the loneliness and isolation he faces there, but Naoko " +
                "finds the pressures and responsibilities of life unbearable. As she retreats further into " +
                "her own world, Toru finds himself reaching out to others and drawn to a fiercely independent " +
                "and sexually liberated young woman.\r\n\r\nA magnificent blending of the music, the mood, " +
                "and the ethos that was the sixties with the story of one college student's romantic coming of " +
                "age, Norwegian Wood brilliantly recaptures a young man's first, hopeless, and heroic love." },

                new DescriptionEntity { Id = 19, Description = "A young woman named Aomame follows a taxi " +
                "driver’s enigmatic suggestion and begins to notice puzzling discrepancies in the world " +
                "around her. She has entered, she realizes, a parallel existence, which she calls 1Q84 —“Q is " +
                "for ‘question mark.’ A world that bears a question.” Meanwhile, an aspiring writer named Tengo " +
                "takes on a suspect ghostwriting project. He becomes so wrapped up with the work and its unusual " +
                "author that, soon, his previously placid life begins to come unraveled." },

                new DescriptionEntity { Id = 20, Description = "Japan's most highly regarded novelist now vaults " +
                "into the first ranks of international fiction writers with this heroically imaginative novel, " +
                "which is at once a detective story, an account of a disintegrating marriage, and an excavation " +
                "of the buried secrets of World War II.\r\n\r\nIn a Tokyo suburb a young man named Toru Okada " +
                "searches for his wife's missing cat. Soon he finds himself looking for his wife as well in a " +
                "netherworld that lies beneath the placid surface of Tokyo. As these searches intersect, Okada " +
                "encounters a bizarre group of allies and antagonists: a psychic prostitute; a malevolent yet " +
                "mediagenic politician; a cheerfully morbid sixteen-year-old-girl; and an aging war veteran who " +
                "has been permanently changed by the hideous things he witnessed during Japan's forgotten campaign " +
                "in Manchuria." },
            };
        }

        private static IEnumerable<GenreEntity> GetPreconfiguredGenres()
        {
            return new List<GenreEntity>()
            {
                new GenreEntity { Id = 1, Name = "Bildungsroman" },
                new GenreEntity { Id = 2, Name = "Historical" },
                new GenreEntity { Id = 3, Name = "Romance" },
                new GenreEntity { Id = 4, Name = "Tragedy" },
                new GenreEntity { Id = 5, Name = "Adventure" },

                new GenreEntity { Id = 6, Name = "Crime" },
                new GenreEntity { Id = 7, Name = "Mystery" },
                new GenreEntity { Id = 8, Name = "Fantasy" },
                new GenreEntity { Id = 9, Name = "Dystopian" },
                new GenreEntity { Id = 10, Name = "Political" },

                new GenreEntity { Id = 11, Name = "Psychological" },
            };
        }

        private static IEnumerable<WarehouseEntity> GetPreconfiguredWarehouse()
        {
            return new List<WarehouseEntity>()
            {
                new WarehouseEntity { Id = 1,  Quantity = 16, Price = 4.49m },
                new WarehouseEntity { Id = 2,  Quantity = 10, Price = 24.11m },
                new WarehouseEntity { Id = 3,  Quantity = 2, Price = 15.04m },
                new WarehouseEntity { Id = 4,  Quantity = 6, Price = 11.61m },
                new WarehouseEntity { Id = 5,  Quantity = 23, Price = 12.10m },

                new WarehouseEntity { Id = 6,  Quantity = 9, Price = 2.29m },
                new WarehouseEntity { Id = 7,  Quantity = 1, Price = 14.89m },
                new WarehouseEntity { Id = 8,  Quantity = 7, Price = 3.66m },
                new WarehouseEntity { Id = 9,  Quantity = 15, Price = 6.16m },
                new WarehouseEntity { Id = 10, Quantity = 3, Price = 8.32m },

                new WarehouseEntity { Id = 11, Quantity = 35, Price = 11.22m },
                new WarehouseEntity { Id = 12, Quantity = 4, Price = 3.96m },
                new WarehouseEntity { Id = 13, Quantity = 8, Price = 9.15m },
                new WarehouseEntity { Id = 14, Quantity = 13, Price = 12.49m },
                new WarehouseEntity { Id = 15, Quantity = 8, Price = 20.99m },

                new WarehouseEntity { Id = 16, Quantity = 13, Price = 14.38m },
                new WarehouseEntity { Id = 17, Quantity = 4, Price = 12.99m },
                new WarehouseEntity { Id = 18, Quantity = 9, Price = 8.49m },
                new WarehouseEntity { Id = 19, Quantity = 6, Price = 15.99m },
                new WarehouseEntity { Id = 20, Quantity = 2, Price = 19.20m },
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
                    PictureName = "we_the_living.png",
                    PagesNumber = 528,
                    Year = 1936,

                    PublisherId = 3,
                    AuthorId = 1,
                    DescriptionId = 1,
                    WarehouseId = 1,
                    LanguageId = 4,
                },
                new BookEntity
                {
                    Id = 2,
                    Title = "Atlas Shrugged",
                    PictureName = "atlas_shrugged.png",
                    PagesNumber = 1168,
                    Year = 1957,

                    PublisherId = 5,
                    AuthorId = 1,
                    DescriptionId = 2,
                    WarehouseId = 2,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 3,
                    Title = "This Side Of Paradise",
                    PictureName = "this_side_of_paradise.png",
                    PagesNumber = 302,
                    Year = 1920,

                    PublisherId = 2,
                    AuthorId = 2,
                    DescriptionId = 3,
                    WarehouseId = 3,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 4,
                    Title = "The Great Gatsby",
                    PictureName = "the_great_gatsby.png",
                    PagesNumber = 184,
                    Year = 1925,

                    PublisherId = 4,
                    AuthorId = 2,
                    DescriptionId = 4,
                    WarehouseId = 4,
                    LanguageId = 4,
                },
                new BookEntity
                {
                    Id = 5,
                    Title = "20,000 Leagues Under the Sea",
                    PictureName = "20000_leagues.png",
                    PagesNumber = 518,
                    Year = 1870,

                    PublisherId = 3,
                    AuthorId = 3,
                    DescriptionId = 5,
                    WarehouseId = 5,
                    LanguageId = 2,
                },

                new BookEntity
                {
                    Id = 6,
                    Title = "The Mysterious Island",
                    PictureName = "the_mysterious_island.png",
                    PagesNumber = 336,
                    Year = 1875,

                    PublisherId = 2,
                    AuthorId = 3,
                    DescriptionId = 6,
                    WarehouseId = 6,
                    LanguageId = 4,
                },
                new BookEntity
                {
                    Id = 7,
                    Title = "The Mighty Orinoco",
                    PictureName = "the_mighty_orinoco.png",
                    PagesNumber = 448,
                    Year = 1898,

                    PublisherId = 3,
                    AuthorId = 3,
                    DescriptionId = 7,
                    WarehouseId = 7,
                    LanguageId = 2,
                },
                new BookEntity
                {
                    Id = 8,
                    Title = "Murder on the Orient Express",
                    PictureName = "murder_on_the_orient_express.png",
                    PagesNumber = 288,
                    Year = 1934,

                    PublisherId = 1,
                    AuthorId = 4,
                    DescriptionId = 8,
                    WarehouseId = 8,
                    LanguageId = 4,
                },
                new BookEntity
                {
                    Id = 9,
                    Title = "And Then There Were None",
                    PictureName = "and_then_there_were_none.png",
                    PagesNumber = 300,
                    Year = 1939,

                    PublisherId = 4,
                    AuthorId = 4,
                    DescriptionId = 9,
                    WarehouseId = 9,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 10,
                    Title = "The Body in the Library",
                    PictureName = "the_body_in_the_library.png",
                    PagesNumber = 245,
                    Year = 1942,

                    PublisherId = 3,
                    AuthorId = 4,
                    DescriptionId = 10,
                    WarehouseId = 10,
                    LanguageId = 4,
                },

                new BookEntity
                {
                    Id = 11,
                    Title = "1984",
                    PictureName = "1984.png",
                    PagesNumber = 328,
                    Year = 1949,

                    PublisherId = 2,
                    AuthorId = 5,
                    DescriptionId = 11,
                    WarehouseId = 11,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 12,
                    Title = "Animal Farm",
                    PictureName = "animal_farm.png",
                    PagesNumber = 140,
                    Year = 1945,

                    PublisherId = 5,
                    AuthorId = 5,
                    DescriptionId = 12,
                    WarehouseId = 12,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 13,
                    Title = "Misery",
                    PictureName = "misery.png",
                    PagesNumber = 310,
                    Year = 1987,

                    PublisherId = 5,
                    AuthorId = 6,
                    DescriptionId = 13,
                    WarehouseId = 13,
                    LanguageId = 4,
                },
                new BookEntity
                {
                    Id = 14,
                    Title = "The Shining",
                    PictureName = "the_shining.png",
                    PagesNumber = 447,
                    Year = 1977,

                    PublisherId = 2,
                    AuthorId = 6,
                    DescriptionId = 14,
                    WarehouseId = 14,
                    LanguageId = 2,
                },
                new BookEntity
                {
                    Id = 15,
                    Title = "A Game of Thrones",
                    PictureName = "got.png",
                    PagesNumber = 694,
                    Year = 1996,

                    PublisherId = 1,
                    AuthorId = 7,
                    DescriptionId = 15,
                    WarehouseId = 15,
                    LanguageId = 4,
                },

                new BookEntity
                {
                    Id = 16,
                    Title = "A Clash of Kings",
                    PictureName = "a_clash_of_kings.png",
                    PagesNumber = 761,
                    Year = 1998,

                    PublisherId = 1,
                    AuthorId = 7,
                    DescriptionId = 16,
                    WarehouseId = 16,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 17,
                    Title = "A Storm of Swords",
                    PictureName = "a_sword_of_swords.png",
                    PagesNumber = 973,
                    Year = 2000,

                    PublisherId = 1,
                    AuthorId = 7,
                    DescriptionId = 17,
                    WarehouseId = 17,
                    LanguageId = 1,
                },
                new BookEntity
                {
                    Id = 18,
                    Title = "Norwegian Wood",
                    PictureName = "norwegian_wood.png",
                    PagesNumber = 389,
                    Year = 1987,

                    PublisherId = 2,
                    AuthorId = 8,
                    DescriptionId = 18,
                    WarehouseId = 18,
                    LanguageId = 3,
                },
                new BookEntity
                {
                    Id = 19,
                    Title = "1Q84",
                    PictureName = "1q84.png",
                    PagesNumber = 928,
                    Year = 2009,

                    PublisherId = 2,
                    AuthorId = 8,
                    DescriptionId = 19,
                    WarehouseId = 19,
                    LanguageId = 3,
                },
                new BookEntity
                {
                    Id = 20,
                    Title = "The Wind-Up Bird Chronicle",
                    PictureName = "the_wind-up_bird_chronicle.png",
                    PagesNumber = 607,
                    Year = 1997,

                    PublisherId = 2,
                    AuthorId = 8,
                    DescriptionId = 20,
                    WarehouseId = 20,
                    LanguageId = 3,
                },
            };
        }

        private static IEnumerable<BookGenreEntity> GetPreconfiguredBookGenres()
        {
            return new List<BookGenreEntity>()
            {
                new BookGenreEntity { BookId = 1, GenreId = 2 },
                new BookGenreEntity { BookId = 2, GenreId = 7 },
                new BookGenreEntity { BookId = 2, GenreId = 8 },
                new BookGenreEntity { BookId = 3, GenreId = 1 },
                new BookGenreEntity { BookId = 4, GenreId = 4 },
                new BookGenreEntity { BookId = 5, GenreId = 5 },


                new BookGenreEntity { BookId = 6, GenreId = 5 },
                new BookGenreEntity { BookId = 7, GenreId = 5 },
                new BookGenreEntity { BookId = 8, GenreId = 6 },
                new BookGenreEntity { BookId = 9, GenreId = 6 },
                new BookGenreEntity { BookId = 9, GenreId = 7 },
                new BookGenreEntity { BookId = 10, GenreId = 6 },


                new BookGenreEntity { BookId = 11, GenreId = 9 },
                new BookGenreEntity { BookId = 11, GenreId = 10 },
                new BookGenreEntity { BookId = 12, GenreId = 10 },
                new BookGenreEntity { BookId = 13, GenreId = 11 },
                new BookGenreEntity { BookId = 14, GenreId = 11 },
                new BookGenreEntity { BookId = 15, GenreId = 8 },
                new BookGenreEntity { BookId = 15, GenreId = 10 },


                new BookGenreEntity { BookId = 16, GenreId = 8 },
                new BookGenreEntity { BookId = 16, GenreId = 10 },
                new BookGenreEntity { BookId = 17, GenreId = 8 },
                new BookGenreEntity { BookId = 17, GenreId = 10 },
                new BookGenreEntity { BookId = 18, GenreId = 3 },
                new BookGenreEntity { BookId = 19, GenreId = 7 },
                new BookGenreEntity { BookId = 20, GenreId = 8 },
            };
        }
    }
}
