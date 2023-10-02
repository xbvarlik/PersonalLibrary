using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.RoleDTOs;

public class RoleCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}