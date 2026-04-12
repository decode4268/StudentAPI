using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Helper;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                   code = 400, 
                   msg = "No File Uploaded!"
                });
            }

            // check directory is exist or not, if not exist then it will create a new directory.
            var uploadPath = CreateFileDirectory.CreateUploadFileDirectory("Uploads");

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadPath, fileName);

            // Save File in the created directory 
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                  await file.CopyToAsync(stream);
            }
            return Ok(new
            {
                FileName = file.FileName,
                SavedFileName = fileName,
                Path = filePath
            });
        }
    }
}
