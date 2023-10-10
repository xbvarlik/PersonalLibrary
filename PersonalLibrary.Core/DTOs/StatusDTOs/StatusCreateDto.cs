using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.StatusDTOs;

public class StatusCreateDto : ICreateDto
{
    public string Description { get; set; } = null!;
}