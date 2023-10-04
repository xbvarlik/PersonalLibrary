using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.DTOs.StatusDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.API.Services;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Controllers;


public class StatusController : BaseController<Status, StatusCreateDto, StatusReadDto, StatusUpdateDto, StatusQueryFilterDto>
{
    public StatusController(StatusService service, StatusMapper mapper) : base(service, mapper)
    {
    }

    [HttpPost]
    [Authorize(Roles = "Super Admin")]
    public override Task<IActionResult> CreateAsync(StatusCreateDto dto)
    {
        return base.CreateAsync(dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Super Admin")]
    public override Task<IActionResult> UpdateAsync(int id, StatusUpdateDto dto)
    {
        return base.UpdateAsync(id, dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Super Admin")]
    public override Task<IActionResult> DeleteAsync(int id)
    {
        return base.DeleteAsync(id);
    }
}