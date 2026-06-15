
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingApi.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    public class Controller : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok();  
        }
    }
}
