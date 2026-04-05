using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentAPI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/student")]
    [ApiController]
    public class StudentV2Controller : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Version = "V2",
                Name = "Rahul",
                Email = "rahul@gmail.com"
            });
        }
    }
}
