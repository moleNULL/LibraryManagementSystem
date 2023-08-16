using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Librarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BookLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LibrarianId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLoans_Librarians_LibrarianId",
                        column: x => x.LibrarianId,
                        principalTable: "Librarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLoans_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGenres",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGenres", x => new { x.StudentId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_StudentGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGenres_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PagesNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    DescriptionId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    BookLoanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_BookLoans_BookLoanId",
                        column: x => x.BookLoanId,
                        principalTable: "BookLoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Descriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Descriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Ayn", "Rand" },
                    { 2, "F. Scott", "Fitzgerald" },
                    { 3, "Jules", "Verne" },
                    { 4, "Agatha", "Christie" },
                    { 5, "George", "Orwell" },
                    { 6, "Stephen", "King" },
                    { 7, "George", "Martin" },
                    { 8, "Haruki", "Murakami" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kyiv" },
                    { 2, "Kharkiv" },
                    { 3, "Poltava" },
                    { 4, "Lviv" },
                    { 5, "Dnipro" },
                    { 6, "Uzhgorod" },
                    { 7, "Ivano-Frankivsk" },
                    { 8, "Zaporizhzhia" },
                    { 9, "Kherson" },
                    { 10, "Sumy" }
                });

            migrationBuilder.InsertData(
                table: "Descriptions",
                columns: new[] { "Id", "BookId", "Description" },
                values: new object[,]
                {
                    { 1, 1, "We the Living is not a story of politics, but of the men and women who have to struggle for existence behind the Red banners and slogans. It is a picture of what those slogans do to human beings. What happens to the defiant ones? What happens to those who succumb?\r\n\r\nAgainst a vivid panorama of political revolution and personal revolt, Ayn Rand shows what the theory of socialism means in practice. " },
                    { 2, 2, "Who is John Galt? When he says that he will stop the motor of the world, is he a destroyer or a liberator? Why does he have to fight his battles not against his enemies but against those who need him most? Why does he fight his hardest battle against the woman he loves?\r\n\r\nYou will know the answer to these questions when you discover the reason behind the baffling events that play havoc with the lives of the amazing men and women in this book. You will discover why a productive genius becomes a worthless playboy...why a great steel industrialist is working for his own destruction...why a composer gives up his career on the night of his triumph...why a beautiful woman who runs a transcontinental railroad falls in love with the man she has sworn to kill." },
                    { 3, 3, "\"I don't want to repeat my innocence. I want the pleasure of losing it again.\"\r\nYoung Amory Blaine, who is convinced he has an exceptionally promising future, finishes boarding school and attends Princeton. At university, Amory is an indifferent student, preferring instead to fall in and out of love, and cheerfully immersing himself in a glitzy world of excessive drinking and casual liaisons.\r\nWritten at the tender age of twenty-three, F. Scott Fitzgerald's first novel mirrors some of his own experiences at Princeton University. This romance of the early Jazz Age is a commentary on how love can be affected by money and social status." },
                    { 4, 4, "\"The Great Gatsby\" is a classic novel written by F. Scott Fitzgerald. Set in the lavish and glamorous world of 1920s America, the story followsJay Gatsby, a mysterious and enigmatic millionaire, through the eyes of Nick Carraway, a young Midwesterner who becomes Gatsby's neighbor. The book delves into themes of love, wealth, decadence, and the illusion of the American Dream." },
                    { 5, 5, "When an unidentified monster threatens international shipping, French oceanographer Pierre Aronnax and his unflappable assistant Conseil join an expedition organized by the US Navy to hunt down and destroy the menace. After months of fruitless searching, they finally grapple with their quarry, but Aronnax, Conseil, and the brash Canadian harpooner Ned Land are thrown overboard in the attack, only to find that the “monster” is actually a futuristic submarine, the Nautilus, commanded by a shadowy, mystical, preternaturally imposing man who calls himself Captain Nemo. Thus begins a journey of 20,000 leagues—nearly 50,000 miles—that will take Captain Nemo, his crew, and these three adventurers on a journey of discovery through undersea forests, coral graveyards, miles-deep trenches, and even the sunken ruins of Atlantis. " },
                    { 6, 6, "A stolen hot air balloon lands an unlikely crew on a mysterious island far from everything they know in this action-packed Jules Verne classic—nowwith an arresting new look!\r\n\r\nFive prisoners of war steal a hot air balloon and escape capture in Virginia during the American Civil War. They fly for several thousand miles before a storm forces them to crash land on an unknown island in the Pacific. There, the marooned men must work together to pool all their knowledge and skill if they wish to survive.\r\n\r\nBut the island has its secrets, and the castaways discover it’s not as deserted as they thought. A mysterious figure has been watching them. Does it bring salvation or even more danger?" },
                    { 7, 7, "The Mighty Orinoco tells the story of a young man's search for his father along the then-uncharted Orinoco River of Venezuela. The text contains all the ingredients of a classic Verne scientific-adventure tale: exploration and discovery, humor and drama, dastardly villains and intrepid heroes, and a host of near-fatal encounters with crocodiles, jungle fever, Indians and outlaws ― all set in a wonderfully exotic locale. The Mighty Orinoco also includes a unique twist that will appeal to feminists ― readers will need to discover it for themselves." },
                    { 8, 8, "Just after midnight, a snowdrift stops the famous Orient Express in its tracks as it travels through the mountainous Balkans. The luxurious train is surprisingly full for the time of the year but, by the morning, it is one passenger fewer. An American tycoon lies dead in his compartment, stabbed a dozen times, his door locked from the inside.\r\n\r\nOne of the passengers is none other than detective Hercule Poirot. On vacation.\r\n\r\nIsolated and with a killer on board, Poirot must identify the murderer—in case he or she decides to strike again." },
                    { 9, 9, "First, there were ten—a curious assortment of strangers summoned as weekend guests to a little private island off the coast of Devon. Their host, an eccentric millionaire unknown to all of them, is nowhere to be found. All that the guests have in common is a wicked past they're unwilling to reveal—and a secret that will seal their fate. For each has been marked for murder. A famous nursery rhyme is framed and hung in every room of the mansion.\n\nWhen they realize that murders are occurring as described in the rhyme, terror mounts. One by one they fall prey. Before the weekend is out, there will be none. Who has choreographed this dastardly scheme? And who will be left to tell the tale? Only the dead are above suspicion." },
                    { 10, 10, "It’s seven in the morning. The Bantrys wake to find the body of a young woman in their library. She is wearing evening dress and heavy make-up, which is now smeared across her cheeks.\r\n\r\nBut who is she? How did she get there? And what is the connection with another dead girl, whose charred remains are later discovered in an abandoned quarry?\r\n\r\nThe respectable Bantrys invite Miss Marple to solve the mystery… before tongues start to wag." },
                    { 11, 11, "The new novel by George Orwell is the major work towards which all his previous writing has pointed. Critics have hailed it as his \"most solid, most brilliant\" work. Though the story of Nineteen Eighty-Four takes place thirty-five years hence, it is in every sense timely. The scene is London, where there has been no new housing since 1950 and where the city-wide slums are called Victory Mansions. Science has abandoned Man for the State. As every citizen knows only too well, war is peace." },
                    { 12, 12, "A farm is taken over by its overworked, mistreated animals. With flaming idealism and stirring slogans, they set out to create a paradise of progress, justice, and equality. Thus the stage is set for one of the most telling satiric fables ever penned –a razor-edged fairy tale for grown-ups that records the evolution from revolution against tyranny to a totalitarianism just as terrible." },
                    { 13, 13, "Paul Sheldon. He's a bestselling novelist who has finally met his biggest fan. Her name is Annie Wilkes and she is more than a rabid reader - she is Paul's nurse, tending his shattered body after an automobile accident. But she is also his captor, keeping him prisoner in her isolated house." },
                    { 14, 14, "Jack Torrance's new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he'll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote...and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old." },
                    { 15, 15, "Long ago, in a time forgotten, a preternatural event threw the seasons out of balance. In a land where summers can last decades and winters a lifetime, trouble is brewing. The cold is returning, and in the frozen wastes to the north of Winterfell, sinister forces are massing beyond the kingdom’s protective Wall. To the south, the king’s powers are failing—his most trusted adviser dead under mysterious circumstances and his enemies emerging from the shadows of the throne. At the center of the conflict lie the Starks of Winterfell, a family as harsh and unyielding as the frozen land they were born to. Now Lord Eddard Stark is reluctantly summoned to serve as the king’s new Hand, an appointment that threatens to sunder not only his family but the kingdom itself." },
                    { 16, 16, "A comet the color of blood and flame cuts across the sky. Two great leaders—Lord Eddard Stark and Robert Baratheon—who hold sway over an age of enforced peace are dead, victims of royal treachery. Now, from the ancient citadel of Dragonstone to the forbidding shores of Winterfell, chaos reigns. Six factions struggle for control of a divided land and the Iron Throne of the Seven Kingdoms, preparing to stake their claims through tempest, turmoil, and war.\r\n\r\nIt is a tale in which brother plots against brother and the dead rise to walk in the night. Here a princess masquerades as an orphan boy; a knight of the mind prepares a poison for a treacherous sorceress; and wild men descend from the Mountains of the Moon to ravage the countryside. Against a backdrop of incest and fratricide, alchemy and murder, victory may go to the men and women possessed of the coldest steel...and the coldest hearts. For when kings clash, the whole land trembles." },
                    { 17, 17, "Of the five contenders for power, one is dead, another in disfavor, and still the wars rage as alliances are made and broken. Joffrey sits on the Iron Throne, the uneasy ruler of the Seven Kingdoms. His most bitter rival, Lord Stannis, stands defeated and disgraced, victim of the sorceress who holds him in her thrall. Young Robb still rules the North from the fortress of Riverrun. Meanwhile, making her way across a blood-drenched continent is the exiled queen, Daenerys, mistress of the only three dragons still left in the world. And as opposing forces manoeuver for the final showdown, an army of barbaric wildlings arrives from the outermost limits of civilization, accompanied by a horde of mythical Others—a supernatural army of the living dead whose animated corpses are unstoppable. As the future of the land hangs in the balance, no one will rest until the Seven Kingdoms have exploded in a veritable storm of swords..." },
                    { 18, 18, "Toru, a quiet and preternaturally serious young college student in Tokyo, is devoted to Naoko, a beautiful and introspective young woman, but their mutual passion is marked by the tragic death of their best friend years before. Toru begins to adapt to campus life and the loneliness and isolation he faces there, but Naoko finds the pressures and responsibilities of life unbearable. As she retreats further into her own world, Toru finds himself reaching out to others and drawn to a fiercely independent and sexually liberated young woman.\r\n\r\nA magnificent blending of the music, the mood, and the ethos that was the sixties with the story of one college student's romantic coming of age, Norwegian Wood brilliantly recaptures a young man's first, hopeless, and heroic love." },
                    { 19, 19, "A young woman named Aomame follows a taxi driver’s enigmatic suggestion and begins to notice puzzling discrepancies in the world around her. She has entered, she realizes, a parallel existence, which she calls 1Q84 —“Q is for ‘question mark.’ A world that bears a question.” Meanwhile, an aspiring writer named Tengo takes on a suspect ghostwriting project. He becomes so wrapped up with the work and its unusual author that, soon, his previously placid life begins to come unraveled." },
                    { 20, 20, "Japan's most highly regarded novelist now vaults into the first ranks of international fiction writers with this heroically imaginative novel, which is at once a detective story, an account of a disintegrating marriage, and an excavation of the buried secrets of World War II.\r\n\r\nIn a Tokyo suburb a young man named Toru Okada searches for his wife's missing cat. Soon he finds himself looking for his wife as well in a netherworld that lies beneath the placid surface of Tokyo. As these searches intersect, Okada encounters a bizarre group of allies and antagonists: a psychic prostitute; a malevolent yet mediagenic politician; a cheerfully morbid sixteen-year-old-girl; and an aging war veteran who has been permanently changed by the hideous things he witnessed during Japan's forgotten campaign in Manchuria." }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bildungsroman" },
                    { 2, "Historical" },
                    { 3, "Romance" },
                    { 4, "Tragedy" },
                    { 5, "Adventure" },
                    { 6, "Crime" },
                    { 7, "Mystery" },
                    { 8, "Fantasy" },
                    { 9, "Dystopian" },
                    { 10, "Political" },
                    { 11, "Psychological" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "French" },
                    { 3, "Japanese" },
                    { 4, "Ukrainian" }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "Email", "EntryDate", "FirstName", "LastName", "PictureName" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2018, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Karina", "Kovalenko", "karina_kovalenko.png" },
                    { 2, "", new DateTime(2011, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roman", "Zozylya", "roman_zozylya.png" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AST" },
                    { 2, "ZNANNIA" },
                    { 3, "Signet" },
                    { 4, "Fingerprint! Publishing" },
                    { 5, "Dutton" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "CityId", "Email", "EntryDate", "FirstName", "LastName", "PictureName" },
                values: new object[,]
                {
                    { 1, "123 Taras Shevchenko Street, Kyiv", null, "christopher.anderson.test@gmail.com", new DateTime(2014, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christopher", "Anderson", "christopher_anderson.png" },
                    { 2, "56 Petro Sahaidachny Street, Poltava", null, "john.mitchell.library@gmail.com", new DateTime(2016, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Mitchell", "john_mitchell.png" },
                    { 3, "89 Lesya Ukrainka, Kharkiv", null, "michael.williams.library@gmail.com", new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Williams", "michael_williams.png" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "BookId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 4.49m, 16 },
                    { 2, 2, 24.11m, 10 },
                    { 3, 3, 15.04m, 2 },
                    { 4, 4, 11.61m, 6 },
                    { 5, 5, 12.10m, 23 },
                    { 6, 6, 2.29m, 9 },
                    { 7, 7, 14.89m, 1 },
                    { 8, 8, 3.66m, 7 },
                    { 9, 9, 6.16m, 15 },
                    { 10, 10, 8.32m, 3 },
                    { 11, 11, 11.22m, 35 },
                    { 12, 12, 3.96m, 4 },
                    { 13, 13, 9.15m, 8 },
                    { 14, 14, 12.49m, 13 },
                    { 15, 15, 20.99m, 8 },
                    { 16, 16, 14.38m, 13 },
                    { 17, 17, 12.99m, 4 },
                    { 18, 18, 8.49m, 9 },
                    { 19, 19, 15.99m, 6 },
                    { 20, 20, 19.20m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookLoanId", "DescriptionId", "LanguageId", "PagesNumber", "PictureName", "PublisherId", "Title", "WarehouseId", "Year" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 4, 528, "we_the_living.png", 3, "We the Living", 1, 1936 },
                    { 2, 1, null, 2, 1, 1168, "atlas_shrugged.png", 5, "Atlas Shrugged", 2, 1957 },
                    { 3, 2, null, 3, 1, 302, "this_side_of_paradise.png", 2, "This Side Of Paradise", 3, 1920 },
                    { 4, 2, null, 4, 4, 184, "the_great_gatsby.png", 4, "The Great Gatsby", 4, 1925 },
                    { 5, 3, null, 5, 2, 518, "20000_leagues.png", 3, "20,000 Leagues Under the Sea", 5, 1870 },
                    { 6, 3, null, 6, 4, 336, "the_mysterious_island.png", 2, "The Mysterious Island", 6, 1875 },
                    { 7, 3, null, 7, 2, 448, "the_mighty_orinoco.png", 3, "The Mighty Orinoco", 7, 1898 },
                    { 8, 4, null, 8, 4, 288, "murder_on_the_orient_express.png", 1, "Murder on the Orient Express", 8, 1934 },
                    { 9, 4, null, 9, 1, 300, "and_then_there_were_none.png", 4, "And Then There Were None", 9, 1939 },
                    { 10, 4, null, 10, 4, 245, "the_body_in_the_library.png", 3, "The Body in the Library", 10, 1942 },
                    { 11, 5, null, 11, 1, 328, "1984.png", 2, "1984", 11, 1949 },
                    { 12, 5, null, 12, 1, 140, "animal_farm.png", 5, "Animal Farm", 12, 1945 },
                    { 13, 6, null, 13, 4, 310, "misery.png", 5, "Misery", 13, 1987 },
                    { 14, 6, null, 14, 2, 447, "the_shining.png", 2, "The Shining", 14, 1977 },
                    { 15, 7, null, 15, 4, 694, "got.png", 1, "A Game of Thrones", 15, 1996 },
                    { 16, 7, null, 16, 1, 761, "a_clash_of_kings.png", 1, "A Clash of Kings", 16, 1998 },
                    { 17, 7, null, 17, 1, 973, "a_sword_of_swords.png", 1, "A Storm of Swords", 17, 2000 },
                    { 18, 8, null, 18, 3, 389, "norwegian_wood.png", 2, "Norwegian Wood", 18, 1987 },
                    { 19, 8, null, 19, 3, 928, "1q84.png", 2, "1Q84", 19, 2009 },
                    { 20, 8, null, 20, 3, 607, "the_wind-up_bird_chronicle.png", 2, "The Wind-Up Bird Chronicle", 20, 1997 }
                });

            migrationBuilder.InsertData(
                table: "StudentGenres",
                columns: new[] { "GenreId", "StudentId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 8, 1 },
                    { 10, 1 },
                    { 5, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 3, 3 },
                    { 6, 3 },
                    { 11, 3 }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 1 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 6 },
                    { 9, 6 },
                    { 9, 7 },
                    { 10, 6 },
                    { 11, 9 },
                    { 11, 10 },
                    { 12, 10 },
                    { 13, 11 },
                    { 14, 11 },
                    { 15, 8 },
                    { 15, 10 },
                    { 16, 8 },
                    { 16, 10 },
                    { 17, 8 },
                    { 17, 10 },
                    { 18, 3 },
                    { 19, 7 },
                    { 20, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_LibrarianId",
                table: "BookLoans",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_StudentId",
                table: "BookLoans",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookLoanId",
                table: "Books",
                column: "BookLoanId",
                unique: true,
                filter: "[BookLoanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_DescriptionId",
                table: "Books",
                column: "DescriptionId",
                unique: true,
                filter: "[DescriptionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_WarehouseId",
                table: "Books",
                column: "WarehouseId",
                unique: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_StudentGenres_GenreId",
                table: "StudentGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CityId",
                table: "Students",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "StudentGenres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "BookLoans");

            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Librarians");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
