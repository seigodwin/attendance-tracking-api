
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
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10)
        {
            var results = await _attendanceService.GetAllAsync();
            return results.Success ? Ok(results) : NotFound(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var results = await _attendanceService.GetByIdAsync(id);
            return results.Success ? Ok(results) : NotFound(results);
        }

        [HttpGet("employee-department")]
        public async Task<IActionResult> FilterByEmployeeDepartMent([FromQuery]string departmentName, [FromQuery] int pageNumber = 1,
                                                       [FromQuery]int pageSize = 10)
        {
            var results = await _attendanceService.FilterByEmployeeDeparmentAsync(departmentName , pageNumber , pageSize);
            return results.Success ? Ok(results) : NotFound(results);
        }

        [HttpGet("date")]
        public async Task<IActionResult> FilterByDate([FromQuery] DateOnly date, [FromQuery] int pageNumber = 1,
                                                       [FromQuery]int pageSize = 10)
        {
            var results = await _attendanceService.FilterByDateAsync(date, pageNumber , pageSize);
            return results.Success ? Ok(results) : NotFound(results);
        }

        [HttpGet("date-intervals")]
        public async Task<IActionResult> FilterByDateIntervals([FromQuery] DateOnly startDate, [FromQuery] DateOnly  endDate,  [FromQuery] int pageNumber = 1,
                                                       [FromQuery]int pageSize = 10)
        {
            var results = await _attendanceService.FilterByDateIntervalAsync(startDate, endDate, pageNumber , pageSize);
            return results.Success ? Ok(results) : NotFound(results);
        }
    }   
}