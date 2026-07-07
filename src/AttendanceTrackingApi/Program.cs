using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Options;
using AttendanceTrackingApi.Services.Application.Implimentations;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Auth.Implimentations;
using AttendanceTrackingApi.Services.Auth.Interface;
using AttendanceTrackingApi.Services.Repository.Implimentations;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var envPath = Path.Combine(builder.Environment.ContentRootPath, ".env");
DotNetEnv.Env.Load(envPath);
builder.Configuration.AddEnvironmentVariables();

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
builder.Services.AddScoped<IAttendanceService , AttendanceService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//Repository services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

//Jwt Options
builder.Services.Configure<JwtOptions>( o =>
{
    o.SECRET = builder.Configuration["JWT_SECRET"] ?? throw new InvalidOperationException("Jwt secret is not configured");
    o.ISSUER = builder.Configuration["JWT_ISSUER"] ?? throw new InvalidOperationException("Jwt Issuer is not configured");
    o.AUDIENCE = builder.Configuration["JWT_AUDIENCE"] ?? throw new InvalidOperationException("Jwt Audience is not configured");
    o.EXPIRATION = int.Parse(builder.Configuration["JWT_EXPIRATION"] ?? throw new InvalidOperationException("Jwt Issuer is not configured"));
});


var redisHost = builder.Configuration["REDIS_HOST"] ?? throw new InvalidOperationException("Redis connection string is not configured");

//Configure Redis connection for auth services
builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisHost));

//Configure IDistributedCacheRedis 
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisHost;
    options.InstanceName = "attendance-tracking-api:";
});


//Jwt Authentication
builder.Services.AddAuthentication( o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT_ISSUER"] ?? throw new InvalidOperationException("Jwt issuer no configured"),
        ValidAudience = builder.Configuration["JWT_AUDIENCE"] ?? throw new InvalidOperationException("Jwt audience not configured"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"] ?? throw new InvalidOperationException("Jwt secret not configured"))) 
    };
});

//Serilog
var loggerConfig = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.Console();

if (builder.Environment.IsDevelopment())
{
    loggerConfig.WriteTo.Seq(
        builder.Configuration["SEQ_CONNECTION_STRING"]
        ?? throw new InvalidOperationException("SEQ_CONNECTION_STRING is not configured."));
}

Serilog.Log.Logger = loggerConfig.CreateLogger();

builder.Host.UseSerilog();

//CORS configuration to allow requests from React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyMethod()                     
              .AllowAnyHeader();                    
    });
});

builder.Services.AddAuthorization();

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
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
