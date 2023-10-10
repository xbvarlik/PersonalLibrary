using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Core.DTOs.UserDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;

namespace PersonalLibrary.API.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly UserMapper _mapper;

    public UserService(AppDbContext context, UserManager<User> userManager, UserMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null) throw new NotFoundException("User not found");

        return user;
    }
    
    public async Task<UserReadDto> CreateUserAsync(UserCreateDto dto)
    {
        var user = _mapper.ToEntity(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);
        
        if (!result.Succeeded) throw new IdentityException("Error creating user");
        
        await _context.SaveChangesAsync();
        return _mapper.ToDto(user);
    }
    
    public async Task<IdentityResult> UpdateUserAsync(int id, UserUpdateDto dto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null) throw new NotFoundException("User not found");
        
        user = _mapper.ToEntity(dto, user);
        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded) throw new IdentityException("Error updating user");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IdentityResult> DeleteUserAsync(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null) throw new NotFoundException("User not found");
        
        var result = await _userManager.DeleteAsync(user);
        
        if (!result.Succeeded) throw new IdentityException("Error deleting user");
        
        await _context.SaveChangesAsync();
        return result;
    }
}