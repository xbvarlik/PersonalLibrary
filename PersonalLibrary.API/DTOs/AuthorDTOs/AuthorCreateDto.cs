using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.AuthorDTOs;

public class AuthorCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}