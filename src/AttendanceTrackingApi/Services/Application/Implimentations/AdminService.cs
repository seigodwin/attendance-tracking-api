
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Implimentations
{
    public class AdminService : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AdminService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
                response.Message = $"Failed to delete employee: {ex.Message}";
            }
            return response;
        }

        public async Task<BaseResponse<List<RegisterEmployeeResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var response = new BaseResponse<List<RegisterEmployeeResponseDto>>();

            try
            {
                var employees = await _employeeRepository.GetAllAsync(1 , 10);

                if (!employees.Any())
                {
                    response.Success = false;
                    response.Message = "Employees not found";
                    return response;
                }

                response.Data = employees.Select(e => new RegisterEmployeeResponseDto
                {
                    id = e.id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Department = e.Department
                }).ToList();

                response.Message = "Data retrieved successfully";

            }

            catch( Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occured: {ex.Message}";
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
                    id = employee.id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department
                };
                response.Message = "Data retrived successfully";
            }

            catch(Exception ex)
            {
                response.Message = $"Failed to fetch data: {ex.Message}";
                response.Success = false;
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
                Department = dto.Department
            };

            try
            {
                await _employeeRepository.AddAsync(employee);

                response.Data = new RegisterEmployeeResponseDto
                {
                    id = employee.id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department
                };
                response.Message = "Employee created successfully";
            }
            catch(Exception ex)
            {
                response.Message = $"Failed to create employee: {ex.Message}";
                response.Success = false;
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
                Department = dto.Department
            };

            try
            {
                await _employeeRepository.UpdateAsync(employee);

                response.Message = "Update successful";
            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"Update failed: {ex.Message}";
            }

            return response;
        }
    }
}