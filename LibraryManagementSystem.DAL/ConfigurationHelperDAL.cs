using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.DAL
{
    public static class ConfigurationHelperDAL
    {
        public static string? GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("DbConnection");
        }
    }
}
