using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await _dbContext.Students.ToListAsync();
            return data;
        }
        [HttpGet("GetStudentById{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var data = _dbContext.Students.FirstOrDefault(x => x.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            return data;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudents), new { id = student }, student);
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var existingData = await _dbContext.Students.FindAsync(student.Id);
            if (existingData == null)
                return NotFound();
            existingData.Name = student.Name;
            existingData.Email = student.Email;
            existingData.Age = student.Age;

            await _dbContext.SaveChangesAsync();
            return Ok(existingData);

        }

        [HttpPost("DeleteStudent{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var existingData = await _dbContext.Students.FindAsync(id);
            if (existingData == null)
                return NotFound();
             _dbContext.Remove(existingData);
            await _dbContext.SaveChangesAsync();
            return Ok(existingData);

        }
    }
}
