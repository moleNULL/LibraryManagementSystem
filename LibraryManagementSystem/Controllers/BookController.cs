using AutoMapper;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var booksDto = await _bookService.GetBooksAsync();
            var books = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            return Ok(books);
        }
    }
}
