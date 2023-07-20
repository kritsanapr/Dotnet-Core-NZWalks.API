using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // http://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly string[] students = new string[] {"test", "test2"};
        // GET : https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }
    }
}
