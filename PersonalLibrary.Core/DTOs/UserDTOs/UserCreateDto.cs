using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.UserDTOs;

public class UserCreateDto : ICreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}