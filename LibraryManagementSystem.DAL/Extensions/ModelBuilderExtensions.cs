﻿using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            BookSeedExtensions.Seed(modelBuilder);
        }
    }
}
