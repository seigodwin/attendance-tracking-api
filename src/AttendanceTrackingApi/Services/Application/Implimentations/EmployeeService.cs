
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using AttendanceTrackingApi.Utilities;
using Microsoft.Extensions.Logging;

namespace AttendanceTrackingApi.Services.Application.Implimentations
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
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
        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var response = new BaseResponse<string>();
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee is null)
            {
                response.Success = false;
                response.Message = "Employee not found";
                return response;
            }

            try
            {
                await _employeeRepository.DeleteAsync(employee);
                response.Message = "Delete successful";
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee with id {EmployeeId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to delete employee. Please try again later.";
            }
            return response;
        }

        public async Task<BaseResponse<List<RegisterEmployeeResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var response = new BaseResponse<List<RegisterEmployeeResponseDto>>();

            try
            {
                pageNumber = pageNumber < 1 ? 1 : pageNumber;
                pageSize = pageSize < 1 ? 10 : (pageSize > 30 ? 30 : pageSize);
                
                var employees = await _employeeRepository.GetAllAsync(pageNumber , pageSize);

                if (!employees.Any())
                {
                    response.Success = false;
                    response.Message = "Employees not found";
                    return response;
                }

                response.Data = employees.Select(e => new RegisterEmployeeResponseDto
                {
                    id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    StaffId = e.StaffId,
                    PhoneNumber = e.PhoneNumber,
                    Department = e.Department
                }).ToList();

                response.Message = "Data retrieved successfully";

            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employees. PageNumber: {PageNumber}, PageSize: {PageSize}. Exception chain: {ExceptionDetails}", pageNumber, pageSize, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "An error occurred while retrieving employees. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<RegisterEmployeeResponseDto>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<RegisterEmployeeResponseDto>();

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if(employee is null)
                {
                    response.Message = "Employee not found";
                    response.Success = false;
                    return response;
                }

                response.Data = new RegisterEmployeeResponseDto
                {
                    id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    StaffId = employee.StaffId,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department
                };
                response.Message = "Data retrived successfully";
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee with id {EmployeeId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to fetch employee data. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<RegisterEmployeeResponseDto>> RegisterAsync(RegisterEmployeeRequestDto dto)
        {
            var response = new BaseResponse<RegisterEmployeeResponseDto>();

            if(dto is null)
            {
                response.Success = false;
                response.Message = "Provide valid data to continue";
                return response;
            }

            Employee employee = new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Department = dto.Department,
                StaffId = dto.StaffId
            };

            try
            {
                await _employeeRepository.AddAsync(employee);

                response.Data = new RegisterEmployeeResponseDto
                {
                    id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    StaffId = employee.StaffId,
                    Department = employee.Department
                };
                response.Message = "Employee created successfully";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating employee for email {Email}. Exception chain: {ExceptionDetails}", dto?.Email, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to create employee. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<string>> UpdateAsync(int id, UpdateEmployeeRequestDto dto)
        {
            var response = new BaseResponse<string>();

            if(dto is null)
            {
                response.Message = "Provide valid data to continue";
                response.Success = false;
                return response;
            }

            Employee employee = new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Department = dto.Department,
                StaffId = dto.StaffId
            };

            try
            {
                await _employeeRepository.UpdateAsync(employee);

                response.Message = "Update successful";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee with id {EmployeeId}. Exception chain: {ExceptionDetails}", id, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Update failed. Please try again later.";
            }

            return response;
        }
    }
}