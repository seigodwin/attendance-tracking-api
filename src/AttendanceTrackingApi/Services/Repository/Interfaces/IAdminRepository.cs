
public interface IAdminRepository
{
        Task AddAsync(Admin model , string PassWord);
        Task<Admin?> GetById(string id);
        Task<List<Admin>> GetAllAsync(int pageNumber = 1, int pageSize = 10); 
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, Admin model);
        Task<bool> RoleExistsAsync(string roleName , Admin model);
        Task<bool> AddToRolesAsync(string roleName , Admin model);
}
