namespace LibraryManagementSystem.Helpers
{
    public static class ConfigurationHelper
    {
        public static string? GetConfigurationString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("DbConnection");
        }
    }
}
