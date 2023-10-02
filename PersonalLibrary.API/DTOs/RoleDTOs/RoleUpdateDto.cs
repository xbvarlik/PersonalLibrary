using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.RoleDTOs;

public class RoleUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}