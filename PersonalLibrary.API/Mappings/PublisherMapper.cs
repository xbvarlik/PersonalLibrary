using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class PublisherMapper : BaseMapper<Publisher, PublisherCreateDto, PublisherReadDto, PublisherUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } = new() { nameof(Publisher.Books) };

    public override Publisher ToEntity(PublisherCreateDto dto)
    {
        return new Publisher
        {
            Name = dto.Name
        };
    }

    public override Publisher ToEntity(PublisherUpdateDto dto, Publisher entity)
    {
        entity.Name = dto.Name ?? entity.Name;
        return entity;
    }

    protected override PublisherReadDto MapOtherProperties(Publisher entity)
    {
        return new PublisherReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };    
    }
}