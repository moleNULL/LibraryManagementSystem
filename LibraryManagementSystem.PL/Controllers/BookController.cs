using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.PL.Models.Responses;
using LibraryManagementSystem.PL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagementSystem.PL.Controllers
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
        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var booksDto = await _bookService.GetBooksAsync();
            var books = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookViewModel), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(AddResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(BookAddViewModel bookViewModel)
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
        [ProducesResponseType(typeof(UpdateResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(BookUpdateViewModel bookViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookViewModel);

            await _bookService.UpdateBookAsync(bookDto);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BookDeleteViewModel booksToDeleteViewModel)
        {
            var bookIds = booksToDeleteViewModel.BookIds.Select(bookId => bookId);
            await _bookService.DeleteBooksAsync(bookIds);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookByIdAsync(id);

            return Ok();
        }
    }
}
