using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.StatusDTOs;

public class StatusUpdateDto : IUpdateDto
{
    public string? Description { get; set; } 
}