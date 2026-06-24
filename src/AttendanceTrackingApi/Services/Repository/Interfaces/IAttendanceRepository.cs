
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Services.Repository.Interfaces
{
    public interface IAttendanceRepository
    {
        Task AddAsync (Attendance model);   
        Task<Attendance?> GetByIdAsync(int id);    
        Task<List<Attendance>> GetAllAsync (int pageNumber , int pageSize);   
        Task DeleteAsync(Attendance model);
    }
}