
using AttendanceTrackingApi.Services.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingApi.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeService = employeeServices;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10)
        {
            var response = await _employeeService.GetAllAsync(1 , 10);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _employeeService.GetByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _employeeService.DeleteAsync(id);

            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeRequestDto dto)
        {
           if(dto is not null && ModelState.IsValid)
            {
                 var response = await _employeeService.UpdateAsync(id, dto);

                return response.Success ? Ok(response) : NotFound(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterEmployeeRequestDto dto)
        {
           if(dto is not null && ModelState.IsValid)
            {
                var response = await _employeeService.RegisterAsync(dto);

                return response.Success ? CreatedAtAction(nameof(GetById), new {id = response.Data!.id} , response)
                : BadRequest(response);
            }

            return BadRequest(ModelState);
        }
    }
}
