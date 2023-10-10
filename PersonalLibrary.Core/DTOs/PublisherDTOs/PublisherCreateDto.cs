using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.PublisherDTOs;

public class PublisherCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}