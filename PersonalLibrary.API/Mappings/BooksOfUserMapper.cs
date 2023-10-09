using PersonalLibrary.API.DTOs.BooksOfUserDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class BooksOfUserMapper : BaseMapper<BooksOfUser, BooksOfUserCreateDto, BooksOfUserReadDto, BooksOfUserUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } =
        new() { nameof(BooksOfUser.User), nameof(BooksOfUser.Book), nameof(BooksOfUser.Status), nameof(BooksOfUser.TagsOfUser) };

    public override BooksOfUser ToEntity(BooksOfUserCreateDto dto)
    {
        return new BooksOfUser
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
            StatusId = dto.StatusId,
            TagsOfUserId = dto.TagsOfUserId
        };
    }

    public override BooksOfUser ToEntity(BooksOfUserUpdateDto dto, BooksOfUser entity)
    {
        entity.UserId = dto.UserId ?? entity.UserId;
        entity.BookId = dto.BookId ?? entity.BookId;
        entity.StatusId = dto.StatusId ?? entity.StatusId;
        entity.TagsOfUserId = dto.TagsOfUserId ?? entity.TagsOfUserId;

        return entity;
    }

    protected override BooksOfUserReadDto MapOtherProperties(BooksOfUser entity)
    {
        return new BooksOfUserReadDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            BookId = entity.BookId,
            StatusId = entity.StatusId,
            TagsOfUserId = entity.TagsOfUserId
        };
    }
    
    public override BooksOfUserReadDto ToDto(BooksOfUser entity, bool includeNavigationProperties)
    {
        var dto = MapOtherProperties(entity);
        if (!includeNavigationProperties) return dto;
        
        dto.User = new UserMapper().ToDto(entity.User);
        dto.Book = new BookMapper().ToDto(entity.Book, false);
        dto.Status = new StatusMapper().ToDto(entity.Status, false);
        dto.TagsOfUser = new TagsOfUserMapper().ToDto(entity.TagsOfUser, false);

        return dto;
    }
}