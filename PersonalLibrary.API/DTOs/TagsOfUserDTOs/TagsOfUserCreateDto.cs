using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.TagsOfUserDTOs;

public class TagsOfUserCreateDto : ICreateDto
{
    public string TagName { get; set; } = null!;

    public string UserId { get; set; } = null!;
}