
public class AdminRepository : IAdminRepository
{
    public Task AddAsync(Admin model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddToRolesAsync(string roleName, Admin model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Admin>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public Task<Admin?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RoleExistsAsync(string roleName, Admin model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(string id, Admin model)
    {
        throw new NotImplementedException();
    }
}