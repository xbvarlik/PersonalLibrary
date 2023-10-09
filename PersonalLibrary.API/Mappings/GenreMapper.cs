using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.DTOs.GenreDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class GenreMapper : BaseMapper<Genre, GenreCreateDto, GenreReadDto, GenreUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } = new() { nameof(Genre.Books) };

    public override Genre ToEntity(GenreCreateDto dto)
    {
        return new Genre
        {
            Name = dto.Name
        };
    }

    public override Genre ToEntity(GenreUpdateDto dto, Genre entity)
    {
        entity.Name = dto.Name ?? entity.Name;
        return entity;
    }

    protected override GenreReadDto MapOtherProperties(Genre entity)
    {
        return new GenreReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}