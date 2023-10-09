using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class AuthorMapper : BaseMapper<Author, AuthorCreateDto, AuthorReadDto, AuthorUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } = new () { nameof(Author.Books) };

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

    protected override AuthorReadDto MapOtherProperties(Author entity)
    {
        return new AuthorReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}




