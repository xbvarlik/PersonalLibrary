using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.PublisherDTOs;

public class PublisherUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}