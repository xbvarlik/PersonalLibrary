using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.AuthorDTOs;

public class AuthorUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}