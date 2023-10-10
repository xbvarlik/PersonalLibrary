using PersonalLibrary.Core.DTOs.Base;
using PersonalLibrary.Core.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.Core.DTOs.UserDTOs;

public class UserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; }
}