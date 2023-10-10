using PersonalLibrary.Core.DTOs.TagsOfUserDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Mappings;

public class TagsOfUserMapper : BaseMapper<TagsOfUser, TagsOfUserCreateDto, TagsOfUserReadDto, TagsOfUserUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } = 
        new() { nameof(TagsOfUser.BooksOfUsers), nameof(TagsOfUser.User) };

    public override TagsOfUser ToEntity(TagsOfUserCreateDto dto)
    {
        return new TagsOfUser
        {
            TagName = dto.TagName
        };
    }

    public override TagsOfUser ToEntity(TagsOfUserUpdateDto dto, TagsOfUser entity)
    {
        entity.TagName = dto.TagName ?? entity.TagName;

        return entity;
    }

    protected override TagsOfUserReadDto MapOtherProperties(TagsOfUser entity)
    {
        return new TagsOfUserReadDto
        {
            Id = entity.Id,
            TagName = entity.TagName,
            UserId = entity.UserId
        };
    }

    public override TagsOfUserReadDto ToDto(TagsOfUser entity, bool includeNavigationProperties)
    {
        var dto = MapOtherProperties(entity);

        if (includeNavigationProperties)
            dto.Books = entity.BooksOfUsers?.Select(x => new BooksOfUserMapper().ToDto(x, false)).ToList();
        
        return dto;
    }
}