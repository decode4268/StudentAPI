using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.Helper;
using StudentAPI.Model;
using StudentAPI.Repository;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        //private readonly IRepository<Student> _student;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        //private readonly IValidator<StudentCustomFluentValidation> _validator;

        public StudentController(IUnitOfWork unitOfWork
            , ApplicationDbContext context,
           ITokenService tokenService /*, IValidator<StudentCustomFluentValidation> validator*/)
        {
            _unitOfWork = unitOfWork;
            //_validator = validator;
            _tokenService = tokenService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(string userName, string password)
        {
            var data = _context.Students.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            if (data != null)
            {
                var token = _tokenService.GenerateToken(userName, data.Role); // Access Token
                var refreshToken = _tokenService.GenerateRefreshToken();
                data.RefreshToken = refreshToken;
                data.RefreshTokenExpiry = DateTime.Now.AddDays(7);
                _context.SaveChanges();
                // Refresh token
                return Ok(new
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser(Student student)
        {
            var response = new
            {
                status = 400,
                msg = "user already exist in the system"
            };
            var data = _context.Students.Where(x => x.Email == student.Email).FirstOrDefault();
            if (data != null)
            {
                return Ok(response);

            }
            _context.Students.Add(student);
            if (_context.SaveChanges() > 0)
            {

                return Ok(new
                {
                    status = 200,
                    msg = "user registered successfully"
                });
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refreshToken")]
        public IActionResult RefreshToken(string refreshToken)
        {
            var userData = _context.Students.FirstOrDefault(x => x.RefreshToken == refreshToken);
            if (userData == null || userData.RefreshTokenExpiry <= DateTime.Now)
            {
                return Unauthorized("Invalid refresh token");
            }
            var newAccessToken = _tokenService.GenerateToken(userData.UserName, userData.Role);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            userData.RefreshToken = newRefreshToken;
            userData.RefreshTokenExpiry = DateTime.Now.AddDays(7);
            _context.SaveChanges();
            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });

        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return await _unitOfWork.students.GetAll();
        }
        [HttpGet("GetStudentById{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var data = await _unitOfWork.students.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var data = await _unitOfWork.students.AddStudent(student);
            if (data != false)
            {
                return CreatedAtAction(nameof(GetStudents), new { id = student }, student);
            }
            return BadRequest();
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var data = await _unitOfWork.students.UpdateStudent(student);
            if (data == true)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Updated SuccessFully"
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
            var data = await _unitOfWork.students.DeleteStudent(id);
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

        // Using here Fluent validation
        //[HttpPost("fludentValidator")]
        //public async Task<IActionResult> FludentValidator(StudentCustomFluentValidation request)
        //{
        //    var validationResult = await _validator.ValidateAsync(request);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);
        //    }

        //    // You can write logic here for if validation is correct..
        //    return Ok("User registered successfully.");
        //}
    }
}
