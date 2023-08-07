using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.BookViewModels1;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.BookControllers
{
    [Route("api/v1/books/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var booksDto = await _bookService.GetBooksAsync();
                var booksViewModel = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

                return Ok(booksViewModel);    
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching books");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the book");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add(BookAddViewModel bookToAddViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookToAddViewModel);

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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update( int id, BookUpdateViewModel bookToUpdateViewModel)
        {
            var bookDto = _mapper.Map<BookDto>(bookToUpdateViewModel);
            bookDto.Id = id;
            bookDto.Warehouse.BookId = id;

            try
            {
                bool isUpdated = await _bookService.UpdateBookAsync(bookDto);
                return Ok(isUpdated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
