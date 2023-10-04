using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.DTOs.CommunicationDTOs;
using PersonalLibrary.API.DTOs.RoleDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;

namespace PersonalLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Super Admin")]
public class RoleController : ControllerBase
{
    private readonly RoleService _service;
    private readonly RoleMapper _mapper;

    public RoleController(RoleService service, RoleMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var roles = await _service.GetRolesAsync();
        if (roles.Count == 0) return NoContent();
        
        var dtos = roles.Select(x => _mapper.ToDto(x)).ToList();
        var response = ResponseDto<List<RoleReadDto>>.Success(200, dtos);
        
        return CreateActionResult(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var role = await _service.GetRoleByIdAsync(id);
        
        var dto = _mapper.ToDto(role);
        var response = ResponseDto<RoleReadDto>.Success(200, dto);
        
        return CreateActionResult(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(RoleCreateDto dto)
    {
        var role = await _service.CreateRoleAsync(dto);
        
        var response = ResponseDto<RoleReadDto>.Success(201, role);
        
        return CreateActionResult(response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, RoleUpdateDto dto)
    {
        await _service.UpdateRoleAsync(id, dto);
        
        var response = ResponseDto<RoleReadDto>.Success(204);
        
        return CreateActionResult(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteRoleAsync(id);
        
        var response = ResponseDto<RoleReadDto>.Success(204);
        
        return CreateActionResult(response);
    }
    
    [NonAction] 
    private IActionResult CreateActionResult<T>(ResponseDto<T> response) 
    { 
        if (response.StatusCode == 204) 
            return new ObjectResult(null) 
            { 
                StatusCode = response.StatusCode 
            };
        return new ObjectResult(response) 
        { 
            StatusCode = response.StatusCode 
        };
    }
}