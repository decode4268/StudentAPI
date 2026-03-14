using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.Model;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepository _student;
        public StudentController(IRepository student)
        {
            _student = student;
        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return await _student.GetAll();
        }
        [HttpGet("GetStudentById{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var data = await _student.GetById(id);
            if(data == null)
            {
                return NotFound();
            }
            return data;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
           var data = await _student.AddStudent(student);
            if (data != false)
            {
                return CreatedAtAction(nameof(GetStudents), new { id = student }, student);
            }
            return BadRequest();
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var data = await _student.UpdateStudent(student);
            if (data == true)
            {
                return Ok(new
                {
                    code = 200,
                    message ="Data Updated SuccessFully"
                });
            }
            return BadRequest(new
            {
                code = 400,
                message = "Something went wrong at out end, please try again later after some time"
            });

        }

        [HttpPost("DeleteStudent{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var data = await _student.DeleteStudent(id);
            if (data == true)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data removed SuccessFully"
                });
            }
            return BadRequest(new
            {
                code = 400,
                message = "Something went wrong at out end, please try again later after some time"
            });

        }
    }
}
