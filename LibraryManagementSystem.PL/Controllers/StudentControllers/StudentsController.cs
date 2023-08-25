using System.Net;
using AutoMapper;
using LibraryManagementSystem.BLL.Exceptions;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Services.Interfaces.StudentServiceInterfaces;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.StudentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers.StudentControllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentsController(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }
    
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var studentsDto = await _studentService.GetStudentsAsync();
                var studentsViewModel = _mapper.Map<IEnumerable<StudentViewModel>>(studentsDto);

                return Ok(studentsViewModel);    
            }
            catch (Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError, 
                    $"An error occurred while fetching students: {ex.Message}");
            }
        }
    
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StudentViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var studentDto = await _studentService.GetStudentByIdAsync(id);
                if (studentDto is not null)
                {
                    var studentViewModel = _mapper.Map<StudentDto, StudentViewModel>(studentDto);
                    return Ok(studentViewModel);
                }

                return NotFound($"There is no student with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the student");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add(StudentAddViewModel studentToAddViewModel)
        {
            var studentDto = _mapper.Map<StudentAddViewModel, StudentDto>(studentToAddViewModel);
        
            try
            {
                int insertedId = await _studentService.AddStudentAsync(studentDto);
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
        public async Task<IActionResult> Update(int id, StudentUpdateViewModel studentToUpdateViewModel)
        {
            var studentDto = _mapper.Map<StudentDto>(studentToUpdateViewModel);
            studentDto.Id = id;

            try
            {
                bool isUpdated = await _studentService.UpdateStudentAsync(studentDto);
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
        public async Task<IActionResult> Delete(StudentDeleteViewModel studentsToDeleteViewModel)
        {
            var studentIds = studentsToDeleteViewModel.StudentIds;

            try
            {
                bool areDeleted = await _studentService.DeleteStudentsAsync(studentIds);
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
                bool isUpdated = await _studentService.DeleteStudentByIdAsync(id);
                return Ok(isUpdated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}