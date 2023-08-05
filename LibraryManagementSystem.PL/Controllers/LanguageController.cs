using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces;
using LibraryManagementSystem.PL.ViewModels.GenreViewModels;
using LibraryManagementSystem.PL.ViewModels.LanguageViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers;

[Route($"api/languages")]
[ApiController]
public class LanguageController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILanguageService _languageService;

    public LanguageController(IMapper mapper, ILanguageService languageService)
    {
        _mapper = mapper;
        _languageService = languageService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LanguageViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var languagesDto = await _languageService.GetLanguagesAsync();
            var languagesViewModel = _mapper.Map<IEnumerable<LanguageDto>, IEnumerable<LanguageViewModel>>(languagesDto);
            
            return Ok(languagesViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching languages");
        }
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LanguageViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var languageDto = await _languageService.GetLanguageByIdAsync(id);
            if (languageDto is not null)
            {
                var languageViewModel = _mapper.Map<LanguageDto, LanguageViewModel>(languageDto);
                return Ok(languageViewModel);
            }

            return NotFound($"There is no language with id: {id}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the language");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(LanguageAddUpdateViewModel languageToAddViewModel)
    {
        var languageDto = _mapper.Map<LanguageAddUpdateViewModel, LanguageDto>(languageToAddViewModel);
        
        try
        {
            int insertedId = await _languageService.AddLanguageAsync(languageDto);
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
    public async Task<IActionResult> Update(int id, LanguageAddUpdateViewModel languageToUpdateViewModel)
    {
        var languageDto = _mapper.Map<LanguageDto>(languageToUpdateViewModel);
        languageDto.Id = id;

        try
        {
            bool isUpdated = await _languageService.UpdateLanguageAsync(languageDto);
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
    public async Task<IActionResult> Delete(LanguageDeleteViewModel languagesToDeleteViewModel)
    {
        var languageIds = languagesToDeleteViewModel.LanguageIds;

        try
        {
            bool areDeleted = await _languageService.DeleteLanguageAsync(languageIds);
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
            bool isUpdated = await _languageService.DeleteLanguageByIdAsync(id);
            return Ok(isUpdated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}