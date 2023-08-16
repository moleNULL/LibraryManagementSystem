using LibraryManagementSystem.BLL.Repositories.Interfaces.LibrarianRepositoryInterfaces;
using LibraryManagementSystem.BLL.Repositories.Interfaces.StudentRepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementSystem.PL.Filters;

public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILibrarianRepository _librarianRepository;

    public AuthorizationFilter(IStudentRepository studentRepository, ILibrarianRepository librarianRepository)
    {
        _studentRepository = studentRepository;
        _librarianRepository = librarianRepository;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        string? email = context.HttpContext.User.Identity?.Name;
        if (email is null)
        {
            return;
        }
        
        var librarianEntity = await _librarianRepository.GetLibrarianByEmailAsync(email);
        if (librarianEntity is not null)
        {
            context.HttpContext.Items["IsLibrarian"] = true;
            context.HttpContext.Items["IsStudent"] = false;
            return;
        }
        
        var studentEntity = await _studentRepository.GetStudentByEmailAsync(email);
        if (studentEntity is not null)
        {
            context.HttpContext.Items["IsStudent"] = true;
            context.HttpContext.Items["IsLibrarian"] = false;
            return;
        }
            
        context.Result = new ForbidResult();
    }
}