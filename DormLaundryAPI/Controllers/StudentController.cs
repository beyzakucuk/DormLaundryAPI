using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DormLaundryAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentService service;

        public StudentController(StudentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await service.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                var student = await service.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentForCreationDto studentForCreation)
        {
            try
            {
                var student = await service.CreateStudentAsync(studentForCreation);
                return CreatedAtRoute(new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromBody] ForDeletingDto id)
        {
            try
            {
                var student = await service.DeleteStudentAsync(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
