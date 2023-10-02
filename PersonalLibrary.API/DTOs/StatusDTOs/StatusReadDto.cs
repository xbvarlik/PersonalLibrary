using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.API.DTOs.StatusDTOs;

public class StatusReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string Description { get; set; } = null!;
    
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; } = null!;
}