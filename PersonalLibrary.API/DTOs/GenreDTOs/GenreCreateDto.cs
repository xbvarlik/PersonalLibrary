using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.GenreDTOs;

public class GenreCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}