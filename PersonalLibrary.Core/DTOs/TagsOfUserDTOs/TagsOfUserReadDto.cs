using PersonalLibrary.Core.DTOs.Base;
using PersonalLibrary.Core.DTOs.BooksOfUserDTOs;

namespace PersonalLibrary.Core.DTOs.TagsOfUserDTOs;

public class TagsOfUserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string TagName { get; set; } = null!;
    
    public int UserId { get; set; }
    
    public virtual ICollection<BooksOfUserReadDto>? Books { get; set; } = null!;

}