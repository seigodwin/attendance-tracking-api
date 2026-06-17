using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Services.Application.Implimentations;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Repository.Implimentations;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dbcontext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Db connection string not configured");

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseNpgsql(connectionString);
    o.UseCamelCaseNamingConvention();
});

builder.Services.AddIdentity<Admin, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

//App services
builder.Services.AddScoped<IEmployeeServices ,EmployeeService>();
builder.Services.AddScoped<IAdminService , AdminService>();

//Repository services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("", o =>
    {
        o.Theme = ScalarTheme.DeepSpace;
        o.Title = "Attendace Tracking Api";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
