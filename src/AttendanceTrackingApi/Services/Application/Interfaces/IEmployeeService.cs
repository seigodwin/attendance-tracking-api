
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IEmployeeServices
    {
        Task<BaseResponse<RegisterEmployeeResponseDto>> RegisterAsync(RegisterEmployeeRequestDto dto);
        Task<BaseResponse<List<RegisterEmployeeResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<BaseResponse<RegisterEmployeeResponseDto>> GetByIdAsync(int id);
        Task<BaseResponse<string>> DeleteAsync(int id);
        Task<BaseResponse<string>> UpdateAsync(int id , UpdateEmployeeRequestDto dto);
    }
}