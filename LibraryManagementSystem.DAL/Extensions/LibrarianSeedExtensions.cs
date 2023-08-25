﻿using LibraryManagementSystem.BLL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions
{
    public static class LibrarianSeedExtensions
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibrarianEntity>().HasData(GetPreconfiguredLibrarians());
        }

        private static IEnumerable<LibrarianEntity> GetPreconfiguredLibrarians()
        {
            return new List<LibrarianEntity>()
            {
                new LibrarianEntity()
                {
                    Id = 1,
                    FirstName = "Karina",
                    LastName = "Kovalenko",
                    Email = "",
                    PictureName = "karina_kovalenko.png",
                    EntryDate = new DateTime(2018, 12, 24)
                },
                new LibrarianEntity()
                {
                    Id = 2,
                    FirstName = "Dennis",
                    LastName = "Van.",
                    Email = "destoki1997@gmail.com",
                    PictureName = "roman_zozylya.png",
                    EntryDate = new DateTime(2011, 05, 01)
                },
            };
        }
    }
}