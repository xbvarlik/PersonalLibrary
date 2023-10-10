using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.TagsOfUserDTOs;

public class TagsOfUserUpdateDto : IUpdateDto
{
    public string? TagName { get; set; }
    
    public string? UserId { get; set; }
}