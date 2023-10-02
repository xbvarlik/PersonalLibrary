using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.RoleDTOs;

public class RoleReadDto : IReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}