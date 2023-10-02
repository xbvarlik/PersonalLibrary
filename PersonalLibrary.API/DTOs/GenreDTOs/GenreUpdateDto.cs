using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.GenreDTOs;

public class GenreUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}