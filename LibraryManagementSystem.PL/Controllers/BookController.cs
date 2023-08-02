using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.PL.ViewModels.BookViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagementSystem.PL.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BookController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var booksDto = await _bookService.GetBooksAsync();
            var booksViewModel = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            return Ok(booksViewModel);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var bookDto = await _bookService.GetBookByIdAsync(id);
                if (bookDto is not null)
                {
                    var bookViewModel = _mapper.Map<BookDto, BookViewModel>(bookDto);
                    return Ok(bookViewModel);
                }

                return NotFound($"There is no book with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(BookAddViewModel bookViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookViewModel);

            try
            {
                int insertedId = await _bookService.AddBookAsync(bookDto);
                return Ok(insertedId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update( int id, BookUpdateViewModel bookViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookViewModel);
            bookDto.Id = id;
            bookDto.Warehouse.BookId = id;

            try
            {
                bool isUpdated = await _bookService.UpdateBookAsync(bookDto);
                return Ok(isUpdated);    
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BookDeleteViewModel booksToDeleteViewModel)
        {
            var bookIds = booksToDeleteViewModel.BookIds;

            try
            {
                bool areDeleted = await _bookService.DeleteBooksAsync(bookIds);
                return Ok(areDeleted);   
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isUpdated = await _bookService.DeleteBookByIdAsync(id);
                return Ok(isUpdated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
