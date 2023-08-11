using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.PublisherViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.BookControllers;

[Route("api/v1/publishers")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublisherService _publisherService;

    public PublishersController(IMapper mapper, IPublisherService publisherService)
    {
        _mapper = mapper;
        _publisherService = publisherService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PublisherViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var publishersDto = await _publisherService.GetPublishersAsync();
            var publishersViewModel = _mapper.Map<IEnumerable<PublisherDto>, IEnumerable<PublisherViewModel>>(publishersDto);
            
            return Ok(publishersViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching publishers");
        }
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PublisherViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var publisherDto = await _publisherService.GetPublisherByIdAsync(id);
            if (publisherDto is not null)
            {
                var publisherViewModel = _mapper.Map<PublisherDto, PublisherViewModel>(publisherDto);
                return Ok(publisherViewModel);
            }

            return NotFound($"There is no publisher with id: {id}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the publisher");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(PublisherAddViewModel publisherToAddViewModel)
    {
        var publisherDto = _mapper.Map<PublisherAddViewModel, PublisherDto>(publisherToAddViewModel);
        
        try
        {
            int insertedId = await _publisherService.AddPublisherAsync(publisherDto);
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
    public async Task<IActionResult> Update(int id, PublisherUpdateViewModel publisherToViewModel)
    {
        var publisherDto = _mapper.Map<PublisherDto>(publisherToViewModel);
        publisherDto.Id = id;

        try
        {
            bool isUpdated = await _publisherService.UpdatePublisherAsync(publisherDto);
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
    public async Task<IActionResult> Delete(PublisherDeleteViewModel publishersToDeleteViewModel)
    {
        var publisherIds = publishersToDeleteViewModel.PublisherIds;

        try
        {
            bool areDeleted = await _publisherService.DeletePublishersAsync(publisherIds);
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
            bool isUpdated = await _publisherService.DeletePublisherByIdAsync(id);
            return Ok(isUpdated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}