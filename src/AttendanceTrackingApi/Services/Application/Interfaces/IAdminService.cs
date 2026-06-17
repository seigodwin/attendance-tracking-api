
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Application.Interfaces
{
    public interface IAdminService
    {
        Task<BaseResponse<RegisterAdminResponseDto>> RegisterAsync(RegisterAdminRequestDto dto); 
        Task<BaseResponse<List<RegisterAdminResponseDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<BaseResponse<RegisterAdminResponseDto>> GetByIdAsync(string id); 
        Task<BaseResponse<string>> DeleteAsync(string id); 
        Task<BaseResponse<string>> UpdateAsync(string id , UpdateAdminRequestDto dto); 
        
    }
}