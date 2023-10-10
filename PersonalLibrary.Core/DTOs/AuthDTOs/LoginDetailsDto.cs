using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Core.DTOs.AuthDTOs;

public class LoginDetailsDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime AccessTokenExpiryTime { get; set; }

    [Required]
    public string AccessToken { get; set; } = null!;

    [Required]
    public DateTime RefreshTokenExpiryTime { get; set; }

    [Required]
    public string RefreshToken { get; set; } = null!;
}