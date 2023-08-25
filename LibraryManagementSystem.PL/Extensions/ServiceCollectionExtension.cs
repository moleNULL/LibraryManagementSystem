using LibraryManagementSystem.BLL.Repositories.Interfaces.BookRepositoryInterfaces;
using LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using LibraryManagementSystem.BLL.Services.Implementations.BookServices;
using LibraryManagementSystem.BLL.Services.Implementations.LibrarianServices;
using LibraryManagementSystem.BLL.Services.Implementations.StudentServices;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.LibrarianServiceInterfaces;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;
using LibraryManagementSystem.DAL.Repositories;
using LibraryManagementSystem.DAL.Repositories.BookRepositories;
using LibraryManagementSystem.DAL.Repositories.StudentRepositories;

namespace LibraryManagementSystem.PL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterLibraryBookServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
        
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageService, LanguageService>();

            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            return services;
        }

        public static IServiceCollection RegisterLibraryStudentServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();

            return services;
        }

        public static IServiceCollection RegisterLibraryLibrarianServices(this IServiceCollection services)
        {
            services.AddScoped<ILibrarianRepository, LibrarianRepository>();
            services.AddScoped<ILibrarianService, LibrarianService>();

            return services;
        }
    }
}