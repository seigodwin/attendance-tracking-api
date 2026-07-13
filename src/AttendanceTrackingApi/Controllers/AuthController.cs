
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Auth.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;

        public AuthController(IAdminService adminService, IAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (dto is not null && ModelState.IsValid)
            {
                var response = await _authService.LoginAsync(dto);
                return response.Success ? Ok(response) : BadRequest(response);
            }

            return BadRequest(ModelState);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            if (dto is not null && ModelState.IsValid)
            {
                var response = await _authService.ForgotPasswordAsync(dto);
                return response.Success ? Ok(response) : BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            if (dto is not null && ModelState.IsValid)
            {
                var response = await _authService.ResetPasswordAsync(dto);
                return response.Success ? Ok(response) : BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            if (dto is not null && ModelState.IsValid)
            {
                var response = await _authService.ChangePasswordAsync(dto);
                return response.Success ? Ok(response) : BadRequest(response);
            }

            return BadRequest(ModelState);
        }
    }
}
