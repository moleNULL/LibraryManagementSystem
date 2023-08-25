using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementSystem.PL.Filters
{
    public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue(Constants.USER_ROLE_COOKIE_NAME, out string? userRole);
            
            if (userRole is null)
            {
                context.Result = new ForbidResult();
            }

            return Task.CompletedTask;
        }
    }
}