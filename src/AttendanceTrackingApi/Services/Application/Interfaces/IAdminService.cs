
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAdminServices
    {
        Task<BaseResponse<RegisterAdminResponseDto>> RegisterAsync(RegisterAdminRequestDto dto); 
        Task<BaseResponse<List<RegisterAdminResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<BaseResponse<RegisterAdminResponseDto>> GetByIdAsync(int id); 
        Task<BaseResponse<string>> DeleteAsync(int id); 
        Task<BaseResponse<string>> UpdateAsync(int id , UpdateAdminRequestDto dto); 
    }
}