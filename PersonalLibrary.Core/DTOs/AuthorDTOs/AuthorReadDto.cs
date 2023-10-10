using PersonalLibrary.Core.DTOs.Base;
using PersonalLibrary.Core.DTOs.BookDTOs;

namespace PersonalLibrary.Core.DTOs.AuthorDTOs;

public class AuthorReadDto : IReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<BookReadDto>? Books { get; set; }
}