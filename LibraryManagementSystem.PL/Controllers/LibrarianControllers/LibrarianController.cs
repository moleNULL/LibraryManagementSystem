﻿using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Services.Interfaces.LibrarianServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.LibrarianViewModels.LibrarianViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.LibrarianControllers;

[Route("api/v1/librarians")]
[ApiController]
public class LibrarianController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILibrarianService _librarianService;

    public LibrarianController(IMapper mapper, ILibrarianService librarianService)
    {
        _mapper = mapper;
        _librarianService = librarianService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LibrarianViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var librariansDto = await _librarianService.GetLibrariansAsync();
            var librariansViewModel = _mapper.Map<IEnumerable<LibrarianViewModel>>(librariansDto);

            return Ok(librariansViewModel);    
        }
        catch (Exception ex)
        {
            return StatusCode(
                (int)HttpStatusCode.InternalServerError, 
                $"An error occurred while fetching librarians: {ex.Message}");
        }
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LibrarianViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var librarianDto = await _librarianService.GetLibrarianByIdAsync(id);
            if (librarianDto is not null)
            {
                var librarianViewModel = _mapper.Map<LibrarianViewModel>(librarianDto);
                return Ok(librarianViewModel);
            }

            return NotFound($"There is no librarian with id: {id}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the librarian");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(LibrarianAddViewModel librarianToAddViewModel)
    {
        var librarianDto = _mapper.Map<LibrarianAddViewModel, LibrarianDto>(librarianToAddViewModel);
        
        try
        {
            int insertedId = await _librarianService.AddLibrarianAsync(librarianDto);
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
    public async Task<IActionResult> Update(int id, LibrarianUpdateViewModel librarianToUpdateViewModel)
    {
        var librarianDto = _mapper.Map<LibrarianUpdateViewModel, LibrarianDto>(librarianToUpdateViewModel);
        librarianDto.Id = id;

        try
        {
            bool isUpdated = await _librarianService.UpdateLibrarianAsync(librarianDto);
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
    public async Task<IActionResult> Delete(LibrarianDeleteViewModel librariansToDeleteViewModel)
    {
        var librarianIds = librariansToDeleteViewModel.LibrarianIds;

        try
        {
            bool areDeleted = await _librarianService.DeleteLibrariansAsync(librarianIds);
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
            bool isUpdated = await _librarianService.DeleteLibrarianByIdAsync(id);
            return Ok(isUpdated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}