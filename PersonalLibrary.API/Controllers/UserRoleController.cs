using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.Services;

namespace PersonalLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly UserRoleService _service;

    public UserRoleController(UserRoleService service)
    {
        _service = service;
    }
    
    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetUserRolesByUserIdAsync(int userId)
    {
        var roles = await _service.GetUserRolesAsync(userId);
        return Ok(roles);
    }
    
    [HttpPost("{roleId}/users")]
    public async Task<IActionResult> GetRoleUsersByRoleIdAsync(int roleId)
    {
        var result = await _service.GetRoleUsersAsync(roleId);
        return Ok(result);
    }
    
    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddUserToRoleAsync(int userId, int roleId)
    {
        var result = await _service.AddUserToRoleAsync(userId, roleId);
        return Ok(result);
    }
    
    [HttpDelete("{userId}/roles")]
    public async Task<IActionResult> RemoveUserFromRoleAsync(int userId, int roleId)
    {
        var result = await _service.RemoveUserFromRoleAsync(userId, roleId);
        return Ok(result);
    }
    
}