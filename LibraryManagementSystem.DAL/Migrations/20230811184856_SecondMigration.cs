using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email", "EntryDate", "PictureName" },
                values: new object[] { "123 Taras Shevchenko Street, Kyiv", "christopher.anderson.test@gmail.com", new DateTime(2023, 8, 11, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(453), "christopher_anderson.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Email", "EntryDate" },
                values: new object[] { "56 Petro Sahaidachny Street, Poltava", "john.mitchell.library@gmail.com", new DateTime(2023, 8, 10, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(517) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Email", "EntryDate" },
                values: new object[] { "89 Lesya Ukrainka, Kharkiv", "michael.williams.library@gmail.com", new DateTime(2023, 8, 9, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(524) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email", "EntryDate", "PictureName" },
                values: new object[] { "", "", new DateTime(2023, 8, 9, 11, 57, 2, 25, DateTimeKind.Local).AddTicks(4447), "christopher_andersen.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Email", "EntryDate" },
                values: new object[] { "", "", new DateTime(2023, 8, 9, 11, 57, 2, 25, DateTimeKind.Local).AddTicks(4665) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Email", "EntryDate" },
                values: new object[] { "", "", new DateTime(2023, 8, 9, 11, 57, 2, 25, DateTimeKind.Local).AddTicks(4680) });
        }
    }
}
