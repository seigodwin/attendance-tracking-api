
using AttendanceTrackingApi.Domain.Dtos.Attendance;
using AttendanceTrackingApi.Domain.Entities;
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<BaseResponse<string>> CheckInAsync(CheckInOrOutRequestDto dto); 
        Task<BaseResponse<string>> CheckoutAsync(CheckInOrOutRequestDto dto);  
        Task<BaseResponse<List<GetAttendanceResponseDto>>> GetAllAsync (AttendanceQueryParameters model, int pageNumber = 1, int PageSize = 10); 
        Task<BaseResponse<GetAttendanceResponseDto?>> GetByIdAsync (int id); 
    }
}

