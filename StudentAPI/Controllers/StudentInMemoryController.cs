using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInMemoryController : ControllerBase
    {
        private  readonly string errMsg = "record already exist with same id";
        private  readonly string deleteMsg = "Data Delete Successfully";
        static List<Student> students = new List<Student>()
        {
            new Student { Id = 1, Name="Deepraj", Email="deepraj@gmail.com", Age=20}
        };

        [HttpGet("GetStudents")]
        public ActionResult<List<Student>> GetStudents()
        {
            return students;
        }

        //[HttpGet]
        //public IActionResult GetAllStudents()
        //{
        //    return Ok(students);
        //}

        [HttpGet("GetStudentById{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            // task if id is provided wrong then return data not found
            var student = students.FirstOrDefault(x => x.Id == id);
            return student;
        }

        [HttpPost("Addnewstudent")]
        public IActionResult AddStudent(Student student)
        {
            // Check the id 
            // if id is already exist in the  students
            // then show msg record already exist with id.
            var data = students.Where(x => x.Id == student.Id);
            if (data.Count() > 0)      // data.Any();
            {
                return Ok(new
                {
                    this.errMsg
                });
            }
            else
            {
                students.Add(student);
                return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
            }
        }

        [HttpPut("UpdateStudentDtl")]
        public IActionResult UpdateStudent(Student student)
        {
            var existingData = students.FirstOrDefault(x => x.Id == student.Id);
            if (existingData == null)
            {
                return NotFound(new
                {
                    this.errMsg
                });
            }
            existingData.Name = student.Name;
            existingData.Email = student.Email;
            existingData.Age = student.Age;
            return Ok(existingData);
        }

        [HttpDelete("DeleteStudent{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(y => y.Id == id);
            if (student == null)
            {
                return NotFound(new
                {
                    errMsg = "Data not found with provided id!"
                });
            }
            students.Remove(student);
            return Ok(new
            {
                this.deleteMsg
            });
        }
    }
}
