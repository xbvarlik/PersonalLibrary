using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalLibrary.Core.Entities;

public class AppRefreshToken 
{
    public int Id { get; set; }
    [ForeignKey("AppUserId")]
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpirationDateTime { get; set; }
}