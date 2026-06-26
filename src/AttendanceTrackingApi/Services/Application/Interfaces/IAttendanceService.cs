
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task CheckInAsync(CheckInOrOutRequestDto dto); 
        Task CheckoutAsync(int id , CheckInOrOutRequestDto dto);  
        Task<List<Attendance>> GetAllAsync (int pageNumber = 1, int PageSize = 10); 
        Task<Attendance?> GetByIdAsync (int id); 
        Task<List<Attendance>> FilterByDateAsync (DateTime date, int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>> FilterByDateIntervalAsync (DateTime firstDate, DateTime secondDate ,
        int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>> FilterByDeparmentAsync (string departmentName , int pageNumber = 1, int PageSize = 10);
    }
}