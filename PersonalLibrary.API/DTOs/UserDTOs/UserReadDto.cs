using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.UserDTOs;

public class UserReadDto : IReadDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}