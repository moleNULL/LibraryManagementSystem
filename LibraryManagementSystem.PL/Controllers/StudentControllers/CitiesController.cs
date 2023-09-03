using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.CityViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.StudentControllers
{
    [Route("api/v1/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;

        public CitiesController(IMapper mapper, ICityService cityService)
        {
            _mapper = mapper;
            _cityService = cityService;
        }
    
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var citiesDto = await _cityService.GetCitiesAsync();
                var citiesViewModel = _mapper.Map<IEnumerable<CityViewModel>>(citiesDto);

                return Ok(citiesViewModel);    
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"An error occurred while fetching cities: {ex.Message}");
            }
        }
    
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CityViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cityDto = await _cityService.GetCityByIdAsync(id);
                if (cityDto is not null)
                {
                    var cityViewModel = _mapper.Map<CityViewModel>(cityDto);
                    return Ok(cityViewModel);
                }

                return NotFound($"There is no city with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the city");
            }
        }
    
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CityAddViewModel cityToAddViewModel)
        {
            var cityDto = _mapper.Map<CityAddViewModel, CityDto>(cityToAddViewModel);
        
            try
            {
                int insertedId = await _cityService.AddCityAsync(cityDto);
                return Ok(insertedId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CityUpdateViewModel cityToUpdateViewModel)
        {
            var cityDto = _mapper.Map<CityUpdateViewModel, CityDto>(cityToUpdateViewModel);
            cityDto.Id = id;

            try
            {
                bool isUpdated = await _cityService.UpdateCityAsync(cityDto);
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
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(CityDeleteViewModel citiesToDeleteViewModel)
        {
            var cityIds = citiesToDeleteViewModel.CityIds;

            try
            {
                bool areDeleted = await _cityService.DeleteCitiesAsync(cityIds);
                return Ok(areDeleted);   
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isUpdated = await _cityService.DeleteCityByIdAsync(id);
                return Ok(isUpdated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}