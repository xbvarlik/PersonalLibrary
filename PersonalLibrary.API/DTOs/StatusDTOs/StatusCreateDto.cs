using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.StatusDTOs;

public class StatusCreateDto : ICreateDto
{
    public string Description { get; set; } = null!;
}