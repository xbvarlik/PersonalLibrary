using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.RoleDTOs;

public class RoleCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}