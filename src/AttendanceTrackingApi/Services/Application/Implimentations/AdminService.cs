
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using AttendanceTrackingApi.Utilities;
using Microsoft.Extensions.Logging;

namespace AttendanceTrackingApi.Services.Application.Implimentations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IAdminRepository adminRepository, ILogger<AdminService> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }

        private static string BuildExceptionDetails(Exception exception)
        {
            var details = new List<string>();
            var current = exception;

            while (current is not null)
            {
                details.Add($"{current.GetType().Name}: {current.Message}");
                current = current.InnerException;
            }

            return string.Join(" -> ", details);
        }

        public async Task<BaseResponse<string>> DeleteAsync(string id)
        {
            var response = new BaseResponse<string>();

            try
            {
                var admin = await _adminRepository.GetByIdAsync(id);
                if(admin is null)
                {
                    response.Success = false;
                    response.Message = "Admin not found";
                    return response;
                }

                await _adminRepository.DeleteAsync(admin);

                response.Message = "Admin deleted successfully";
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting admin with id {AdminId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to delete admin. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<List<RegisterAdminResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var response = new BaseResponse<List<RegisterAdminResponseDto>>();

            try
            {
                var admins = await _adminRepository.GetAllAsync(pageNumber , pageSize);

                if (!admins.Any())
                {
                    response.Success = false;
                    response.Message = "No records found";
                    return response;
                }

                response.Data = admins.Select( a => new RegisterAdminResponseDto
                {
                    id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email ?? string.Empty,
                    PhoneNumber = a.PhoneNumber ?? string.Empty
                }).ToList();              
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving admins. PageNumber: {PageNumber}, PageSize: {PageSize}. Exception chain: {ExceptionDetails}", pageNumber, pageSize, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to retrieve admin data. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<RegisterAdminResponseDto>> GetByIdAsync(string id)
        {
            var response = new BaseResponse<RegisterAdminResponseDto>();

            try
            {
                var admin = await _adminRepository.GetByIdAsync(id);

                if(admin is null)
                {
                    response.Success = false;
                    response.Message = "Record not found";
                    return response;
                }

                response.Data = new RegisterAdminResponseDto
                {
                    id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email ?? string.Empty,
                    PhoneNumber = admin.PhoneNumber ?? string.Empty
                };

                response.Message = "Admin retrived successfully";
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving admin with id {AdminId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to retrieve admin data. Please try again later.";
            }
            return response;
        }

        public async Task<BaseResponse<RegisterAdminResponseDto>> RegisterAsync(RegisterAdminRequestDto dto)
        {
            var response = new BaseResponse<RegisterAdminResponseDto>();

            if(dto is null)
            {
                response.Success = false;
                response.Message = "Provide valid data to continue";
                return response;
            }


            if(dto.PassWord != dto.ConfrimPassWord)
            {
                response.Success = false;
                response.Message = "Passwords do not match";
                return response;
            }

            Admin admin = new()
            {
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };

            try
            {
                var createResult = await _adminRepository.AddAsync(admin, dto.PassWord);

                if (!createResult.Succeeded)
                {
                    response.Success = false;
                    response.Message = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    return response;
                }

                response.Data = new RegisterAdminResponseDto
                {
                    id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email ?? string.Empty,
                    PhoneNumber = admin.PhoneNumber
                };

                response.Message = "Admin created successfully";
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating admin for email {Email}. Exception chain: {ExceptionDetails}", dto?.Email, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to create admin. Please try again later.";
            }
            
            return response;
        }

     

        public async Task<BaseResponse<string>> UpdateAsync(string id, UpdateAdminRequestDto dto)
        {
            var response = new BaseResponse<string>();

            if(dto is null)
            {
                response.Success = false;
                response.Message = "Provide valid data to continue";
                return response;
            }

            try
            {
                var admin = await _adminRepository.GetByIdAsync(id);

                if(admin is null)
                {
                    response.Success = false;
                    response.Message = "Record not found";
                    return response;
                }

                admin.FirstName = dto.FirstName;
                admin.LastName = dto.LastName;
                admin.Email = dto.Email;
                admin.PhoneNumber = dto.PhoneNumber;

                await _adminRepository.UpdateAsync(admin);

                response.Message = "Update successful";

            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating admin with id {AdminId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to update admin. Please try again later.";
            }

            return response;
        }
    }
}