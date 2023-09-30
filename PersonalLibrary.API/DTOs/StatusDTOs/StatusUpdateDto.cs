using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.StatusDTOs;

public class StatusUpdateDto : IUpdateDto
{
    public string? Description { get; set; } 
}