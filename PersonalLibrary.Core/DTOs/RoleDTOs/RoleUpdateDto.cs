using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.RoleDTOs;

public class RoleUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}