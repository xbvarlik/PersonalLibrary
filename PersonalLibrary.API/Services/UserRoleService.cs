using Microsoft.AspNetCore.Identity;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class UserRoleService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    
    public UserRoleService(AppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IdentityResult> AddUserToRoleAsync(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (user == null) throw new NotFoundException("User not found");
        if (role == null) throw new NotFoundException("Role not found");
        if (await IsUserInRoleAsync(user, role.Name!)) throw new IdentityException("User is already in role");
        
        var result = await _userManager.AddToRoleAsync(user, role.Name!);
        
        if (!result.Succeeded) throw new IdentityException("Error adding user to role");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IdentityResult> RemoveUserFromRoleAsync(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (user == null) throw new NotFoundException("User not found");
        if (role == null) throw new NotFoundException("Role not found");
        if (!await IsUserInRoleAsync(user, role.Name!)) throw new IdentityException("User is not in role");
        
        var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);
        
        if (!result.Succeeded) throw new IdentityException("Error removing user from role");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null) throw new NotFoundException("User not found");
        
        return await _userManager.GetRolesAsync(user);
    }
    
    public async Task<List<User>> GetRoleUsersAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (role == null) throw new NotFoundException("Role not found");

        return (await _userManager.GetUsersInRoleAsync(role.Name!) as List<User>)!;
    }

    public async Task<bool> IsUserInRoleAsync(User user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }
}