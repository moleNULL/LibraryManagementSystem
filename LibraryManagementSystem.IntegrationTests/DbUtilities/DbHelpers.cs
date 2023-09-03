using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibraryManagementSystem.IntegrationTests.DbUtilities
{
    public static class DbHelpers
    {
        public static ApplicationDbContext GetApplicationDbContext(string inMemoryDbName)
        {
            return new ApplicationDbContext(GetDbOptions(inMemoryDbName));
        }

        private static DbContextOptions<ApplicationDbContext> GetDbOptions(string inMemoryDbName)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            builder.UseInMemoryDatabase(inMemoryDbName)
                .ConfigureWarnings(options =>
                    options.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}