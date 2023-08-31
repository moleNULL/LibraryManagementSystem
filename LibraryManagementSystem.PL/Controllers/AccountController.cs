using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.StudentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }
        
        [Authorize]
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login()
        {
            string? email = HttpContext.User.Identity?.Name;
            if (email is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Email extraction failed");
            }

            try
            {
                string? userRole = await _accountService.GetUserRoleAsync(email);
                if (userRole is null)
                {
                    return Forbid();
                }
                
                SetCookies(Constants.USER_ROLE_COOKIE_NAME, userRole);
                
                return Ok(userRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the user role");
            }
        }

        [Authorize]
        [HttpPost("register/student")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterStudent(StudentAddViewModel studentViewModel)
        {
            string userRole = Constants.STUDENT_ROLE;
            
            string? email = HttpContext.User.Identity?.Name;
            if (email is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Email extraction failed");
            }

            if (email != studentViewModel.Email)
            {
                return BadRequest($"Email mismatch between Google JWT {email} and form data ${studentViewModel.Email}");
            }

            try
            {
                await _accountService.RegisterStudentAsync(_mapper.Map<StudentDto>(studentViewModel));
                
                SetCookies(Constants.USER_ROLE_COOKIE_NAME, Constants.STUDENT_ROLE);
                
                return Ok(userRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create a student with email: ${email}");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete(Constants.USER_ROLE_COOKIE_NAME);
            return Ok("logged out");
        }
        
        private void SetCookies(string cookieName, string cookieValue)
        {
            HttpContext.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                MaxAge = TimeSpan.FromHours(1),
            });
        }
    }
}