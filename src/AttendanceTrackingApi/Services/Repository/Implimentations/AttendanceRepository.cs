
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Domain.Dtos.Attendance;
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

        public async Task<List<Attendance>> GetAllAsync(AttendanceQueryParameters? model , int pageNumber, int pageSize)
        {
            var queryable = _context.Attendances.AsNoTracking();

            if(model is not null)
            {
                if (!string.IsNullOrEmpty(model.DepartmentName))
                {
                    queryable = queryable.Where(a => a.Employee.Department == model.DepartmentName);
                }

                if (model.Date.HasValue)
                {
                    queryable = queryable.Where(a => a.AttendanceDate == model.Date);
                }

                if (model.StartDate.HasValue && model.EndDate.HasValue)
                {
                    queryable = queryable.Where(a => a.AttendanceDate >= model.StartDate || a.AttendanceDate <= model.EndDate);
                }
            }


            return await queryable.Include(a => a.Employee)       
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

        public async Task UpdateAsync(Attendance model)
        {
            _context.Attendances.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}