
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task CheckInAsync(Attendance model);
        Task CheckoutAsync(int id , Attendance model);
        Task<List<Attendance>?> GetAllAsync (int pageNumber = 1, int PageSize = 10);
        Task<Attendance?> GetByIdAsync (int id);
        Task<List<Attendance>?> FilterByDateAsync (DateTime date, int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>?> FilterByDateIntervalAsync (DateTime firstDate, DateTime secondDate ,
        int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>?> FilterByDeparmentAsync (string departmentName , int pageNumber = 1, int PageSize = 10);
    }
}