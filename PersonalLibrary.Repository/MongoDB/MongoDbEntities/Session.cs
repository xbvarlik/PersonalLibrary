using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Repository.MongoDB.MongoDbEntities;

public class Session<TLogin> where TLogin : class
{
    public string Id { get; set; } = null!;
    public int UserId { get; set; }
    
    public string Agent { get; set; } = null!;
    
    [Required]
    public string Email { get; set; } = null!;
    
    [Required]
    public IList<string> UserRoles { get; set; } = null!;
    
    [Required]
    public TLogin Login { get; set; } = null!;
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public DateTime UpdatedAt { get; set; }
}