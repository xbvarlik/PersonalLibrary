using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.RoleDTOs;

public class RoleReadDto : IReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}