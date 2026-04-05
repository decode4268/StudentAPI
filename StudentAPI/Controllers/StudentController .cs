using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get(ApiVersion version)
        {
            if (version.MajorVersion ==1)
            {
                return Ok(new
                {
                    Version = "V1",
                    Name = "Rahul"
                });
            }
            return Ok(new
            {
                Version = "V2",
                Name = "Rahul",
                Email = "rahul@gmail.com"
            });
        }
    }
}
