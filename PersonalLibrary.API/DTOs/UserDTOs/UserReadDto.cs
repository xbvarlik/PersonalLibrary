using System.Reflection.Metadata;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.API.DTOs.UserDTOs;

public class UserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; }
}