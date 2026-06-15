
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingApi.Services.Repository.Implimentations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Employee model)
        {
            _context.Employees.Remove(model);
            await _context.SaveChangesAsync();
        }
          

        public async Task<List<Employee>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var employees = await _context.Employees.Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task AddAsync(Employee model)
        {
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee model)
        {
            _context.Employees.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}