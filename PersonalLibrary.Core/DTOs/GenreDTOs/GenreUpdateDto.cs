using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.GenreDTOs;

public class GenreUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}