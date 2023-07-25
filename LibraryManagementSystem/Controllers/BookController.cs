using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/books")]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var bookDto = await _bookService.GetBookByIdAsync(id);

            if (bookDto is not null)
            {
                return Ok(bookDto);
            }

            return BadRequest($"There is no book with id: {id}");
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookViewModel bookViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookViewModel);

            try
            {
                await _bookService.AddBookAsync(bookDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookViewModel bookViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookViewModel);

            await _bookService.UpdateBookAsync(bookDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(IEnumerable<int> bookIds)
        {
            await _bookService.DeleteBooksAsync(bookIds);

            return Ok();
        }
    }
}
