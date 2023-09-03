using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryManagementSystem.IntegrationTests.ControllerTests
{
    public class IntegrationTestBase : IAsyncLifetime
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly HttpClient HttpClient;
    
        protected IntegrationTestBase()
        {
            _dbContext = DbHelpers.GetApplicationDbContext("LibraryInMemoryDb");
        
            var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services
                        .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (dbContextDescriptor is not null)
                    {
                        services.Remove(dbContextDescriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });

                    services.AddScoped<ApplicationDbContext>(_ => _dbContext);
                });
            });

            HttpClient = appFactory.CreateClient();
        }
    
        public async Task InitializeAsync()
        {
            _dbContext.Books.AddRange(DbSeeder.GetPreconfiguredBooks());
            await _dbContext.SaveChangesAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}