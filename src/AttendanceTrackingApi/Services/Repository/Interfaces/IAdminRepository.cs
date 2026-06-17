
using Microsoft.AspNetCore.Identity;



public interface IAdminRepository
{
        Task<IdentityResult> AddAsync(Admin model , string PassWord);
        Task<Admin?> GetByIdAsync(string id);
        Task<List<Admin>> GetAllAsync(int pageNumber = 1, int pageSize = 10); 
        Task DeleteAsync(Admin model);
        Task UpdateAsync(Admin model);
        Task AddToRolesAsync(Admin model , List<string> roles);
}
