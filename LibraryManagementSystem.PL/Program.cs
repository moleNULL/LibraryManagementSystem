using LibraryManagementSystem.DAL;
using LibraryManagementSystem.PL.Extensions;
using LibraryManagementSystem.PL.Filters;
using LibraryManagementSystem.PL.Helpers;
using LibraryManagementSystem.PL.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                    .Add(new GoogleTokenValidator(ConfigurationHelper.GetGoogleAuthClientId()));
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
                        .AllowAnyMethod()
                        .WithExposedHeaders("*");
                });
            });

            builder.Services.RegisterLibraryBookServices();
            builder.Services.RegisterLibraryStudentServices();
            builder.Services.RegisterLibraryLibrarianServices();
            
            builder.Services.AddAutoMapper(typeof(Program));

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
    }
}