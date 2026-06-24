
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
 
        builder.Entity<Employee>( entity => 
        {
            entity.HasIndex(e => e.Email).IsUnique();

             entity.HasMany<Attendance>(e => e.Attendances)
            .WithOne( a => a.employee)
            .HasForeignKey(a => a.EmployeeId);
            
            entity.HasData
            (
            new Employee
            {
                id = 1,
                FirstName = "Sei",
                LastName = "Godwin",
                Email = "seigodwin65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "IT"
            },

              new Employee
            {
                id = 2,
                FirstName = "Sei",
                LastName = "Ray",
                Email = "ray65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "Reception"
            },

              new Employee
            {
                id = 3,
                FirstName = "Sei",
                LastName = "Jane",
                Email = "jane65@gmail.com",
                PhoneNumber = "0540580393",
                Department = "IT"
            }
            );
        });

        builder.Entity<Admin>().HasIndex(a =>a.Email).IsUnique();
        builder.Entity<Admin>().HasIndex(a =>a.PhoneNumber).IsUnique();
    }
}
}