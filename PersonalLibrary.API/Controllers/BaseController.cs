using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.CommunicationDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TEntity, TCreateDto, TReadDto, TUpdateDto, TQUeryFilterDto> : ControllerBase
    where TEntity : BaseEntity
    where TCreateDto : class, ICreateDto
    where TReadDto : class, IReadDto
    where TUpdateDto : class, IUpdateDto
    where TQUeryFilterDto : class, IQueryFilterDto
{
    protected readonly BaseService<TEntity, TCreateDto, TReadDto, TUpdateDto, TQUeryFilterDto> Service;
    protected readonly BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto> Mapper;

    protected BaseController(BaseService<TEntity, TCreateDto, TReadDto, TUpdateDto, TQUeryFilterDto> service, BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto> mapper)
    {
        Service = service;
        Mapper = mapper;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync([FromQuery] TQUeryFilterDto query)
    {
        var entities = await Service.GetAllAsync(query);
        if (entities.Count == 0)
            return NoContent();

        var dtos = entities.Select(x => Mapper.ToDto(x, true)).ToList();
        
        var response = ResponseDto<List<TReadDto>>.Success(200, dtos);
        return CreateActionResult(response);
    }
    
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var entity = await Service.GetByIdAsync(id);
        var dto = Mapper.ToDto(entity, true);
        
        var response = ResponseDto<TReadDto>.Success(200, dto);
        return CreateActionResult(response);
    }
    
    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateDto dto)
    {
        var readDto = await Service.CreateAsync(dto);
        
        var response = ResponseDto<TReadDto>.Success(201, readDto);
        return CreateActionResult(response);
    }
    
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TUpdateDto dto)
    {
        await Service.UpdateAsync(id, dto);
        
        var response = ResponseDto<NoContent>.Success(204);
        return CreateActionResult(response);
    }
    
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await Service.DeleteAsync(id);
        
        var response = ResponseDto<NoContent>.Success(204);
        return CreateActionResult(response);
    }

    [NonAction] 
    public IActionResult CreateActionResult<T>(ResponseDto<T> response) 
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