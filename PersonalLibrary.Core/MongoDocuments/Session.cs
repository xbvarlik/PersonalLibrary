using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalLibrary.Core.MongoDocuments;

public class Session<TLogin> where TLogin : class
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = null!; // MongoDB requires this property to be named _id
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