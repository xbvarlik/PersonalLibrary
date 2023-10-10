namespace PersonalLibrary.Core.DTOs.AuthDTOs;

public class AccessTokenDto
{
    public string AccessToken { get; set; } = null!;
    public DateTime AccessTokenExpirationDateTime { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpirationDateTime { get; set; }
}