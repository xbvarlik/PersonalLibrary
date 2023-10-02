using PersonalLibrary.API.DTOs.TagsOfUserDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class TagsOfUserMapper : BaseMapper<TagsOfUser, TagsOfUserCreateDto, TagsOfUserReadDto, TagsOfUserUpdateDto>
{
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

    public override TagsOfUserReadDto ToDto(TagsOfUser entity)
    {
        return new TagsOfUserReadDto
        {
            Id = entity.Id,
            TagName = entity.TagName,
            Books = entity.Books?.Select(b => new BooksOfUserMapper().ToDto(b)).ToList()
        };
    }
}