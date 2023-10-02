using PersonalLibrary.API.DTOs.BooksOfUserDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class BooksOfUserMapper : BaseMapper<BooksOfUser, BooksOfUserCreateDto, BooksOfUserReadDto, BooksOfUserUpdateDto>
{
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

    public override BooksOfUserReadDto ToDto(BooksOfUser entity)
    {
        return new BooksOfUserReadDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            BookId = entity.BookId,
            StatusId = entity.StatusId,
            TagsOfUserId = entity.TagsOfUserId,
            User = entity.User is null ? null : new UserMapper().ToDto(entity.User),
            Book = entity.Book is null ? null : new BookMapper().ToDto(entity.Book),
            Status = entity.Status is null ? null : new StatusMapper().ToDto(entity.Status),
            TagsOfUser = entity.TagsOfUser is null ? null : new TagsOfUserMapper().ToDto(entity.TagsOfUser)
        };
    }
}