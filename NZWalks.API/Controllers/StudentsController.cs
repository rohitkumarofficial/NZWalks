using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        //https://localhost:port/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(new string[] { "Rohit" });
        }
    }
}
