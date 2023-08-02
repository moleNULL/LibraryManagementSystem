using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
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
    public async Task<IActionResult> Get()
    {
        var authorsDto = await _authorService.GetAuthorsAsync();
        var authorViewModel = _mapper.Map<IEnumerable<AuthorDto>, IEnumerable<AuthorViewModel>>(authorsDto);

        return Ok(authorViewModel);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorViewModel), (int)HttpStatusCode.OK)]
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
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AuthorAddUpdateViewModel authorUpdateViewModel)
    {
        var authorDto = _mapper.Map<AuthorAddUpdateViewModel, AuthorDto>(authorUpdateViewModel);
        
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
    public async Task<IActionResult> Update(int id, AuthorAddUpdateViewModel authorUpdateViewModel)
    {
        var authorDto = _mapper.Map<AuthorDto>(authorUpdateViewModel);
        authorDto.Id = id;

        try
        {
            bool isUpdated = await _authorService.UpdateAuthorAsync(authorDto);
            return Ok(isUpdated);    
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
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