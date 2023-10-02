using System.Reflection.Metadata;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.API.DTOs.UserDTOs;

public class UserReadDto : IReadDto
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; }
}