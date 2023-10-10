using PersonalLibrary.Core.DTOs.Base;
using PersonalLibrary.Core.DTOs.BookDTOs;
using PersonalLibrary.Core.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.Core.DTOs.StatusDTOs;

public class StatusReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string Description { get; set; } = null!;
    
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; } = null!;
}