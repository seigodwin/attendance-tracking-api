using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using AttendanceTrackingApi.Services.Application.Interfaces;
using AttendanceTrackingApi.Services.Application.Implimentations;
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Utilities;
using AttendanceTrackingApi.DbContext;
using AttendanceTrackingApi.Services.Repository.Interfaces;
using AttendanceTrackingApi.Domain.Entities;

namespace AttendanceTrackingApi.Tests.Services
{
    public class AttendanceServiceTest
    {
        private readonly AttendanceService _sut;

        public AttendanceServiceTest()
        {
            // In-memory DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "AttendanceTestDb_" + Guid.NewGuid())
                .Options;

            var context = new AppDbContext(options);

            // Seed a sample employee and attendance
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                PhoneNumber = "000",
                Department = "IT",
                StaffId = "S1"
            };

            context.Employees.Add(employee);
            context.Attendances.Add(new Attendance
            {
                Id = 1,
                EmployeeId = employee.Id,
                Employee = employee,
                CheckInTime = TimeOnly.FromDateTime(DateTime.Now),
                AttendanceDate = DateOnly.FromDateTime(DateTime.Now)
            });
            context.SaveChanges();

            // Mock repository to return the attendances from the in-memory context
            var repoMock = new Mock<IAttendanceRepository>();
            repoMock.Setup(r => r.GetAllAsync(It.IsAny<AttendanceQueryParameters>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(context.Attendances.Include(a => a.Employee).ToList());

            _sut = new AttendanceService(repoMock.Object, context);
        }

        [Fact]
        public async Task GetAllAsync_WithNullQueryParameters_ReturnsAllAttendances()
        {
            // Arrange
            var queryParametrs = new AttendanceQueryParameters();

            // Act
            var attendances = await _sut.GetAllAsync(queryParametrs);

            // Assert
            attendances.Should().NotBeNull();
            attendances.Success.Should().BeTrue();
            attendances.Data.Should().NotBeNull();
            attendances.Data.Should().HaveCountGreaterThan(0);
        }
    }
}