
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Services.Repository.Interfaces;
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

        public async Task<List<Attendance>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Attendances.AsNoTracking()
                                              .Include(a => a.Employee)
                                              .OrderByDescending(a => a.CheckInTime)
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