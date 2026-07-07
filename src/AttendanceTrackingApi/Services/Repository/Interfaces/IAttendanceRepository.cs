
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Services.Repository.Interfaces
{
    public interface IAttendanceRepository
    {
        Task AddAsync (Attendance model);  
        Task UpdateAsync(Attendance model); 
        Task<Attendance?> GetByIdAsync(int id);    
        Task<List<Attendance>> GetAllAsync (AttendanceQueryParameters? model , int pageNumber = 1 , int pageSize = 10);   
        Task DeleteAsync(Attendance model);
    }
}