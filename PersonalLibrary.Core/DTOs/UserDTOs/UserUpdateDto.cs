using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.UserDTOs;

public class UserUpdateDto : IUpdateDto
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Email { get; set; } 
    public string? Password { get; set; } 
}