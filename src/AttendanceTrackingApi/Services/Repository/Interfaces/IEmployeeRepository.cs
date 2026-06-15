
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee model);
        Task<List<Employee>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Employee?> GetByIdAsync(int id);
        Task DeleteAsync(Employee model);
        Task UpdateAsync(Employee model);
    }
}