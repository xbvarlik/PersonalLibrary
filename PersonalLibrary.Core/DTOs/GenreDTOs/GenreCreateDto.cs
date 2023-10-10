using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.GenreDTOs;

public class GenreCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}