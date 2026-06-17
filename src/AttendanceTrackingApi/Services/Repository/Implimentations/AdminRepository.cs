
using AttendanceTrackingApi.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<Admin> _userManager;
    private readonly AppDbContext _context;

    public AdminRepository(UserManager<Admin> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IdentityResult> AddAsync(Admin model , string Password)
    {
        var result = await _userManager.CreateAsync(model , Password);
        return result;
    }

    public async Task AddToRolesAsync(Admin model , List<string> roles)
    {
        await _userManager.AddToRolesAsync(model , roles);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Admin model)
    {
        await _userManager.DeleteAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Admin>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _context.Users.Skip((pageNumber -1 ) * pageSize)
                              .Take(pageSize)
                                .ToListAsync();
    }

    public async Task<Admin?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }


    public async Task UpdateAsync(Admin model)
    {
        await _userManager.UpdateAsync(model);
        await _context.SaveChangesAsync();
    }
}