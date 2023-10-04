using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.DTOs.CommunicationDTOs;
using PersonalLibrary.API.DTOs.UserDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    private readonly UserMapper _mapper;
    
    public UserController(UserService service, UserMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] UserQueryFilterDto filter)
    {
        var entities = await _service.GetUsersAsync();
        if (entities.Count == 0) return NoContent();
        
        var dtos = entities.Select(x => _mapper.ToDto(x)).ToList();
        
        var response = ResponseDto<List<UserReadDto>>.Success(200, dtos);
        return CreateActionResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        var dto = _mapper.ToDto(user);
        
        var response = ResponseDto<UserReadDto>.Success(200, dto);
        return CreateActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateDto dto)
    {
        var user = await _service.CreateUserAsync(dto);
        
        var response = ResponseDto<UserReadDto>.Success(201, user);
        return CreateActionResult(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UserUpdateDto dto)
    {
        await _service.UpdateUserAsync(id, dto);
        
        var response = ResponseDto<UserReadDto>.Success(204);
        return CreateActionResult(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteUserAsync(id);
        
        var response = ResponseDto<UserReadDto>.Success(204);
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