
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingApi.Services.Repository.Implimentations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Attendance model)
        {
           await  _context.Attendances.AddAsync(model);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Attendance model)
        {
            _context.Attendances.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Attendance>> FilterByDateAsync(DateOnly date, int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Attendances.AsNoTracking()
                                                .Include(a => a.Employee)
                                                .Where(a => a.AttendanceDate == date)
                                                .OrderByDescending(a => a.Id)
                                                .Skip(( pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
        }
 
        public async Task<List<Attendance>> FilterByDateIntervalAsync(DateOnly startDate, DateOnly endDate, int pageNumber = 1, int PageSize = 10)
        {
            return await _context.Attendances.AsNoTracking()
                                             .Include(a => a.Employee)
                                             .Where(a => a.AttendanceDate >= startDate && a.AttendanceDate <= endDate)
                                             .OrderByDescending(a => a.AttendanceDate)
                                             .Skip(( pageNumber - 1) * PageSize)
                                             .Take(PageSize)
                                             .ToListAsync();
        }

        public async Task<List<Attendance>> FilterByDeparmentAsync(string departmentName, int pageNumber = 1, int PageSize = 10)
        {
             return await _context.Attendances.AsNoTracking()
                                             .Include(a => a.Employee)
                                             .Where(a => a.Employee.Department == departmentName)
                                             .OrderByDescending(a => a.AttendanceDate)
                                             .ThenByDescending(a => a.Id)
                                             .Skip(( pageNumber - 1) * PageSize)
                                             .Take(PageSize)
                                             .ToListAsync();
        }

        public async Task<List<Attendance>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Attendances.AsNoTracking()
                                              .Include(a => a.Employee)
                                              .OrderByDescending(a => a.CheckInTime)
                                              .ThenByDescending(a => a.Id)
                                              .Skip((pageNumber -1) * pageSize)
                                              .Take(pageSize)
                                              .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        { 
            return await _context.Attendances.AsNoTracking()
                                            .Include(a => a.Employee) 
                                            .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}