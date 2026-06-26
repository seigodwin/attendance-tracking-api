
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Services.Repository.Interfaces
{
    public interface IAttendanceRepository
    {
        Task AddAsync (Attendance model);   
        Task<Attendance?> GetByIdAsync(int id);    
        Task<List<Attendance>> GetAllAsync (int pageNumber = 1 , int pageSize = 10);   
        Task DeleteAsync(Attendance model);
        Task<List<Attendance>> FilterByDateAsync (DateOnly date, int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>> FilterByDateIntervalAsync (DateOnly firstDate, DateOnly secondDate ,
         int pageNumber = 1, int PageSize = 10);
        Task<List<Attendance>> FilterByDeparmentAsync (string departmentName , int pageNumber = 1, int PageSize = 10);

    }
}