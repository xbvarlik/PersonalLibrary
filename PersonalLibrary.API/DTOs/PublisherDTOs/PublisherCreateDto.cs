using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.PublisherDTOs;

public class PublisherCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}