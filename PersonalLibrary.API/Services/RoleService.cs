using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.RoleDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class RoleService 
{
    private readonly AppDbContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly RoleMapper _mapper;

    public RoleService(AppDbContext context, RoleManager<Role> roleManager, RoleMapper mapper)
    {
        _context = context;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    public async Task<List<Role>> GetRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }
    
    public async Task<Role> GetRoleByIdAsync(int id)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw new NotFoundException("Role not found");

        return role;
    }
    
    public async Task<RoleReadDto> CreateRoleAsync(RoleCreateDto dto)
    {
        var role = _mapper.MapToEntity(dto);
        var result = await _roleManager.CreateAsync(role);
        
        if (!result.Succeeded) throw new IdentityException("Error creating role");
        
        await _context.SaveChangesAsync();
        return _mapper.MapToDto(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(int id, RoleUpdateDto dto)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw new NotFoundException("Role not found");
        
        role = _mapper.MapToEntity(dto, role);
        var result = await _roleManager.UpdateAsync(role);
        
        if (!result.Succeeded) throw new IdentityException("Error updating role");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IdentityResult> DeleteRoleAsync(int id)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw new NotFoundException("Role not found");
        
        var result = await _roleManager.DeleteAsync(role);
        
        if (!result.Succeeded) throw new IdentityException("Error deleting role");
        
        await _context.SaveChangesAsync();
        return result;
    }
}