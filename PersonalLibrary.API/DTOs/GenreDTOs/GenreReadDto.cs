using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;

namespace PersonalLibrary.API.DTOs.GenreDTOs;

public class GenreReadDto : IReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<BookReadDto>? Books { get; set; }
}