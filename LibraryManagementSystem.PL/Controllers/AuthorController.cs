using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.AuthorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthorService _authorService;
    
    public AuthorController(IMapper mapper, IAuthorService authorService)
    {
        _mapper = mapper;
        _authorService = authorService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuthorViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var authorsDto = await _authorService.GetAuthorsAsync();
            var authorViewModel = _mapper.Map<IEnumerable<AuthorDto>, IEnumerable<AuthorViewModel>>(authorsDto);

            return Ok(authorViewModel);
        }
        catch (Exception ex)
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
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the author");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(AuthorAddUpdateViewModel authorToAddViewModel)
    {
        var authorDto = _mapper.Map<AuthorAddUpdateViewModel, AuthorDto>(authorToAddViewModel);
        
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
    public async Task<IActionResult> Update(int id, AuthorAddUpdateViewModel authorToUpdateViewModel)
    {
        var authorDto = _mapper.Map<AuthorDto>(authorToUpdateViewModel);
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