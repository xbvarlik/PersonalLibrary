using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.AuthorDTOs;

public class AuthorUpdateDto : IUpdateDto
{
    public string? Name { get; set; }
}