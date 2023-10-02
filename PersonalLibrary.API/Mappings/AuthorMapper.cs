using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class AuthorMapper : BaseMapper<Author, AuthorCreateDto, AuthorReadDto, AuthorUpdateDto>
{
    public override Author ToEntity(AuthorCreateDto dto)
    {
        return new Author
        {
            Name = dto.Name
        };
    }

    public override Author ToEntity(AuthorUpdateDto dto, Author entity)
    {
        entity.Name = dto.Name ?? entity.Name;
        return entity;
    }

    public override AuthorReadDto ToDto(Author entity)
    {
        return new AuthorReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Books = entity.Books?.Select(b => new BookMapper().ToDto(b)).ToList()
        };
    }
}




