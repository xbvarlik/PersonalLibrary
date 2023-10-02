using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.API.DTOs.TagsOfUserDTOs;

public class TagsOfUserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string TagName { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; } = null!;

}