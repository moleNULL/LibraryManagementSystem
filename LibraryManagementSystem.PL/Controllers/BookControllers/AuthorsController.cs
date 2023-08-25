using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.AuthorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.BookControllers
{
    [Route("api/v1/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
    
        public AuthorsController(IMapper mapper, IAuthorService authorService)
        {
            _mapper = mapper;
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthorSimpleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var authorsSimpleDto = await _authorService.GetAuthorsAsync();
                var authorSimpleViewModel = _mapper.Map<IEnumerable<AuthorSimpleDto>, IEnumerable<AuthorSimpleViewModel>>(authorsSimpleDto);

                return Ok(authorSimpleViewModel);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching authors");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AuthorViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var authorDto = await _authorService.GetAuthorByIdAsync(id);
                if (authorDto is not null)
                {
                    var authorViewModel = _mapper.Map<AuthorDto, AuthorViewModel>(authorDto);
                    return Ok(authorViewModel);
                }

                return NotFound($"There is no author with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the author");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add(AuthorAddViewModel authorToAddViewModel)
        {
            var authorDto = _mapper.Map<AuthorAddViewModel, AuthorDto>(authorToAddViewModel);
        
            try
            {
                int insertedId = await _authorService.AddAuthorAsync(authorDto);
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
        public async Task<IActionResult> Update(int id, AuthorUpdateViewModel authorToViewModel)
        {
            var authorDto = _mapper.Map<AuthorDto>(authorToViewModel);
            authorDto.Id = id;

            try
            {
                bool isUpdated = await _authorService.UpdateAuthorAsync(authorDto);
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
        public async Task<IActionResult> Delete(AuthorDeleteViewModel authorsToDeleteViewModel)
        {
            var authorIds = authorsToDeleteViewModel.AuthorIds;

            try
            {
                bool areDeleted = await _authorService.DeleteAuthorsAsync(authorIds);
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
                bool isUpdated = await _authorService.DeleteAuthorByIdAsync(id);
                return Ok(isUpdated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}