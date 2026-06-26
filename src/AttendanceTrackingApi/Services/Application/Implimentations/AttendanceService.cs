
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using AttendanceTrackingApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingApi.Services.Application.Implimentations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly AppDbContext _context;
        public AttendanceService(IAttendanceRepository attendanceRepository, AppDbContext context)
        {
            _attendanceRepository = attendanceRepository;
            _context = context;
        }

        public async Task<BaseResponse<string>> CheckInAsync(CheckInOrOutRequestDto dto)
        {
            var response = new BaseResponse<string>();
            if(dto is null)
            {
                response.Success = false;
                response.Message = "Provide valid data to continue";
                return response;
            }

            var user = await _context.Employees.AsNoTracking()
                                               .FirstOrDefaultAsync(e => e.Email == dto.Email);

            if(user is null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.StaffId))
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }
            
            if(user.StaffId != dto.StaffId)
            {
                response.Success = false;
                response.Message = "Invalid email or staff Id";
                return response;
            }


            var activeSignIn = await _context.Attendances.AsNoTracking()
                                                        .FirstOrDefaultAsync(a => a.Id == user.Id);
            if(activeSignIn?.CheckInTime is not null)
            {
                response.Success = false;
                response.Message = "User has an active check in. Please check out and try again";
                return response;
            }

            var currentDateTime = DateTime.Now;

            Attendance attendance = new()
            {
                CheckInTime = TimeOnly.FromDateTime(currentDateTime),
                EmployeeId = user.Id,
                AttendanceDate = DateOnly.FromDateTime(currentDateTime)
            };

            try
            {
                await _attendanceRepository.AddAsync(attendance);

                response.Message = "Check in Successful";
            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"Check in failed: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<string>> CheckoutAsync(CheckInOrOutRequestDto dto)
        {
            var response = new BaseResponse<string>();

            if(dto is null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.StaffId))
            {
                response.Success = false;
                response.Message = "Provide valid data to check out";
                return response;
            }

            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(a => a.Email == dto.Email);
            
            if(employee is null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            var currentDateTime = DateTime.Now;

            var todayAttendance = await _context.Attendances
            .FirstOrDefaultAsync(a => a.EmployeeId == employee.Id && a.AttendanceDate == DateOnly.FromDateTime(currentDateTime));

            if(todayAttendance is null)
            {
                response.Success = false;
                response.Message = "No active check in found. Please check in and try agian";
                return response;
            }

            if(todayAttendance.CheckOutTime is not null)
            {
                response.Success = false;
                response.Message = "You have an active checkout. Please check in and try again";
                return response;
            }

            todayAttendance.CheckOutTime = TimeOnly.FromDateTime(currentDateTime);

            try
            {
                await _attendanceRepository.UpdateAsync(todayAttendance);

                response.Message = "Check out successfull";
            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"Check out failed: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<List<GetAttendanceResponseDto>>> GetAllAsync(AttendanceQueryParameters model, int pageNumber = 1, int PageSize = 10)
        {
            var response = new BaseResponse<List<GetAttendanceResponseDto>>();

            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = PageSize < 1 ? 1 : (PageSize < 30 ? 30 : PageSize);

            try
            {
                 var records = await _attendanceRepository.GetAllAsync(model , pageNumber, PageSize);

                if (!records.Any())
                {
                    response.Success = false;
                    response.Message = "Data not found for the specified department";
                    return response;
                }

                response.Data = records.Select(r => new GetAttendanceResponseDto
                    {
                        Id = r.Id,
                        EmployeeFirstName = r.Employee.FirstName,
                        EmployeeLastName = r.Employee.LastName,
                        EmployeeDepartment = r.Employee.Department,
                        CheckInTime = r.CheckInTime,
                        CheckOutTime = r.CheckOutTime,
                        AttendanceDate = r.AttendanceDate
                }).ToList();

                response.Message = "Records retrived successfully";
            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to fetch records: {ex.Message}";
            }
            return response;
        }

        public async Task<BaseResponse<GetAttendanceResponseDto?>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetAttendanceResponseDto?>();
            try
            {
                var record = await _attendanceRepository.GetByIdAsync(id);

                if (record is null)
                {
                    response.Success = false;
                    response.Message = "Data not found for the specified department";
                    return response;
                }

                response.Data =  new GetAttendanceResponseDto
                {
                    Id = record.Id,
                    EmployeeFirstName = record.Employee.FirstName,
                    EmployeeLastName = record.Employee.LastName,
                    EmployeeDepartment = record.Employee.Department,
                    CheckInTime = record.CheckInTime,
                    CheckOutTime = record.CheckOutTime,
                    AttendanceDate = record.AttendanceDate
                };

                response.Message = "Records retrived successfully";
            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to fetch records: {ex.Message}";
            }

            return response;
        }
    }
}