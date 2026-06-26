
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

            if(user is null)
            {
                response.Success = false;
                response.Message = "Account not found";
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

            // Attendance attendance = new()
            // {
            //     CheckInTime = DateTime.
            // }

            return response;
        }

        public Task<BaseResponse<string>> CheckoutAsync(int id, CheckInOrOutRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Attendance>>> FilterByDateAsync(DateOnly date, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Attendance>>> FilterByDateIntervalAsync(DateOnly firstDate, DateOnly secondDate, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Attendance>>> FilterByDeparmentAsync(string departmentName, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Attendance>>> GetAllAsync(int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Attendance?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}