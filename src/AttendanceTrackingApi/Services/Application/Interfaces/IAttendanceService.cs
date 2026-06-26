
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<BaseResponse<string>> CheckInAsync(CheckInOrOutRequestDto dto); 
        Task<BaseResponse<string>> CheckoutAsync(int id , CheckInOrOutRequestDto dto);  
        Task<BaseResponse<List<Attendance>>> GetAllAsync (int pageNumber = 1, int PageSize = 10); 
        Task<BaseResponse<Attendance?>> GetByIdAsync (int id); 
        Task<BaseResponse<List<Attendance>>> FilterByDateAsync (DateOnly date, int pageNumber = 1, int PageSize = 10);
        Task<BaseResponse<List<Attendance>>> FilterByDateIntervalAsync (DateOnly firstDate, DateOnly secondDate ,
        int pageNumber = 1, int PageSize = 10);
        Task<BaseResponse<List<Attendance>>> FilterByDeparmentAsync (string departmentName , 
                                            int pageNumber = 1, int PageSize = 10);
    }
}