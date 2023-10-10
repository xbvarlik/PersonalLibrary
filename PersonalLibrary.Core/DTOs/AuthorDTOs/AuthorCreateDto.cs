using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.AuthorDTOs;

public class AuthorCreateDto : ICreateDto
{
    public string Name { get; set; } = null!;
}