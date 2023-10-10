using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.TagsOfUserDTOs;

public class TagsOfUserCreateDto : ICreateDto
{
    public string TagName { get; set; } = null!;

    public string UserId { get; set; } = null!;
}