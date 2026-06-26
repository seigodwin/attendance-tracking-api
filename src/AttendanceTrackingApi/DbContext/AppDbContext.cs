
using AttendanceTrackingApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingApi.DbContext
{
    public class AppDbContext : IdentityDbContext<Admin>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Employee> Employees {get;set;} 

    public DbSet<Attendance> Attendances {get ; set;} 
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);        
 
        builder.Entity<Employee>(entity =>   
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Department); 

            entity.HasMany<Attendance>(e => e.Attendances)
            .WithOne( a => a.Employee) 
            .HasForeignKey(a => a.EmployeeId); 
            
            entity.HasData
            (                     
            new Employee     
            {             
                Id = 1,
                FirstName = "Sei",
                LastName = "Godwin",
                Email = "seigodwin65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "IT",
                StaffId = "Ghmis01"
            },

              new Employee
            {
                Id = 2,
                FirstName = "Sei",
                LastName = "Ray",
                Email = "ray65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "Reception",
                StaffId = "Ghims02"
            },

              new Employee
            {
                Id = 3,
                FirstName = "Sei",
                LastName = "Jane",
                Email = "jane65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "IT",
                StaffId = "Ghims03"
            }
            );
        }); 

        builder.Entity<Admin>().HasIndex(a =>a.Email).IsUnique();
        builder.Entity<Admin>().HasIndex(a =>a.PhoneNumber).IsUnique();

        builder.Entity<Attendance>().HasIndex( a => a.AttendanceDate);
    }
}
}