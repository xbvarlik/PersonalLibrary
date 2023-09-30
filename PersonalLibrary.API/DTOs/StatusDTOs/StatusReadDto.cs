using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;

namespace PersonalLibrary.API.DTOs.StatusDTOs;

public class StatusReadDto : IReadDto
{
    public Guid Id { get; set; }
    
    public string Description { get; set; } = null!;
    
    public virtual ICollection<BookReadDto>? Books { get; set; } = null!;
}