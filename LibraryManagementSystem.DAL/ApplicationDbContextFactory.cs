using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryManagementSystem.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string? connectionString = "data source=GARGANTUA-ACER\\WORKSERVER;initial catalog=LibraryManagementSystem;Trusted_Connection=true;multipleactiveresultsets=True;TrustServerCertificate=true;";

            if (connectionString is null)
            {
                throw new Exception("connection string is null");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);
        }
    }
}
