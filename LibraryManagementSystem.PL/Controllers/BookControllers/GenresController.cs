using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.BookServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.GenreViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.BookControllers
{
    [Route("api/v1/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
    
        public GenresController(IMapper mapper, IGenreService genreService)
        {
            _mapper = mapper;
            _genreService = genreService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var genresDto = await _genreService.GetGenresAsync();
                var genresViewModel = _mapper.Map<IEnumerable<GenreDto>, IEnumerable<GenreViewModel>>(genresDto);
            
                return Ok(genresViewModel);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching genres");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GenreViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var genreDto = await _genreService.GetGenreByIdAsync(id);
                if (genreDto is not null)
                {
                    var genreViewModel = _mapper.Map<GenreDto, GenreViewModel>(genreDto);
                    return Ok(genreViewModel);
                }

                return NotFound($"There is no author with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the genre");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add(GenreAddViewModel genreToAddViewModel)
        {
            var genreDto = _mapper.Map<GenreAddViewModel, GenreDto>(genreToAddViewModel);
        
            try
            {
                int insertedId = await _genreService.AddGenreAsync(genreDto);
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
        public async Task<IActionResult> Update(int id, GenreUpdateViewModel genreToUpdateViewModel)
        {
            var genreDto = _mapper.Map<GenreDto>(genreToUpdateViewModel);
            genreDto.Id = id;

            try
            {
                bool isUpdated = await _genreService.UpdateGenreAsync(genreDto);
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
        public async Task<IActionResult> Delete(GenreDeleteViewModel genresToDeleteViewModel)
        {
            var genreIds = genresToDeleteViewModel.GenreIds;

            try
            {
                bool areDeleted = await _genreService.DeleteGenresAsync(genreIds);
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
                bool isUpdated = await _genreService.DeleteGenreByIdAsync(id);
                return Ok(isUpdated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}