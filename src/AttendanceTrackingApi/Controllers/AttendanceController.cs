
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingApi.Controllers
{
    [ApiController]
    [Route("api/v1/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AttendanceQueryParameters model , [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10)
        {
            var results = await _attendanceService.GetAllAsync(model , pageNumber , pageSize);
            return results.Success ? Ok(results) : NotFound(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var results = await _attendanceService.GetByIdAsync(id);
            return results.Success ? Ok(results) : NotFound(results);
        }
       
    }   
}