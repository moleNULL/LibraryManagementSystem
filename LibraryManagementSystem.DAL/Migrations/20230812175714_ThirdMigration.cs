using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "Email", "EntryDate", "FirstName", "LastName", "PictureName" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2018, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Karina", "Kovalenko", "karina_kovalenko.png" },
                    { 2, "", new DateTime(2011, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roman", "Zozylya", "roman_zozylya.png" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryDate",
                value: new DateTime(2023, 8, 12, 20, 57, 12, 921, DateTimeKind.Local).AddTicks(491));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryDate",
                value: new DateTime(2023, 8, 11, 20, 57, 12, 921, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryDate",
                value: new DateTime(2023, 8, 10, 20, 57, 12, 921, DateTimeKind.Local).AddTicks(581));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryDate",
                value: new DateTime(2023, 8, 11, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryDate",
                value: new DateTime(2023, 8, 10, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(517));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryDate",
                value: new DateTime(2023, 8, 9, 21, 48, 55, 943, DateTimeKind.Local).AddTicks(524));
        }
    }
}
