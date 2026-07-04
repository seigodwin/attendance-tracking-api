
using AttendanceTrackingApi.Services.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingApi.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] RegisterAdminRequestDto dto)
        {
            if(dto is not null && ModelState.IsValid)
            {
                var response = await _adminService.RegisterAsync(dto);
                return response.Success ? CreatedAtAction(nameof(GetById) , new {id = response.Data!.id} , response)
                                            : BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id , [FromBody] UpdateAdminRequestDto dto)
        {
            if(dto is not null && ModelState.IsValid)
            {
                var response = await _adminService.UpdateAsync(id, dto);

                return response.Success ? NoContent() : BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10) 
        {
            var response = await _adminService.GetAllAsync(pageNumber , pageSize);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _adminService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete (string id)
        {
            var response = await _adminService.DeleteAsync(id);
            return response.Success ? NoContent() : BadRequest(response);
        }
    }
}
