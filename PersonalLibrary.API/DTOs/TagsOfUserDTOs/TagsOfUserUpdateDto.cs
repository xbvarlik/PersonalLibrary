using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.TagsOfUserDTOs;

public class TagsOfUserUpdateDto : IUpdateDto
{
    public string? TagName { get; set; }
    
    public string? UserId { get; set; }
}