using LibraryManagementSystem.DAL;
using LibraryManagementSystem.DAL.Configurations.BookConfigurations;
using LibraryManagementSystem.PL.Extensions;
using LibraryManagementSystem.PL.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators
                    .Add(new GoogleTokenValidator("461786025188-f0hs6dtdnqmj636r5t8r5ei26vqtn8mb.apps.googleusercontent.com"));
            });
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationHelper.GetConnectionString());
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.RegisterLibraryBookServices();
            builder.Services.RegisterLibraryStudentServices();
            builder.Services.RegisterLibraryLibrarianServices();
            
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.Configure<BookConfiguration>(configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAnyOriginPolicy");
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}