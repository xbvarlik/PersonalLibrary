using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.UserDTOs;

public class UserUpdateDto : IUpdateDto
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Email { get; set; } 
    public string? Password { get; set; } 
}