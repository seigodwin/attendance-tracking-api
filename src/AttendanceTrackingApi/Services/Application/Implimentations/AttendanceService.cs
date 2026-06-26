
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Services.Application.Interfaces;

namespace AttendanceTrackingApi.Services.Application.Implimentations
{
    public class AttendanceService : IAttendanceService
    {
        public Task CheckInAsync(CheckInOrOutRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task CheckoutAsync(int id, CheckInOrOutRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attendance>> FilterByDateAsync(DateTime date, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attendance>> FilterByDateIntervalAsync(DateTime firstDate, DateTime secondDate, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attendance>> FilterByDeparmentAsync(string departmentName, int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attendance>> GetAllAsync(int pageNumber = 1, int PageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Attendance?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}