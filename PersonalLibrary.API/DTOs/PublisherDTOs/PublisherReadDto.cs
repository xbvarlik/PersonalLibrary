using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;

namespace PersonalLibrary.API.DTOs.PublisherDTOs;

public class PublisherReadDto : IReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<BookReadDto>? Books { get; set; }
}