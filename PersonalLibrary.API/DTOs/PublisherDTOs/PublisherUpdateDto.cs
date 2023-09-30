using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.PublisherDTOs;

public class PublisherUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}