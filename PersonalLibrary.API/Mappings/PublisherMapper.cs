using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class PublisherMapper : BaseMapper<Publisher, PublisherCreateDto, PublisherReadDto, PublisherUpdateDto>
{
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

    public override PublisherReadDto ToDto(Publisher entity)
    {
        return new PublisherReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Books = entity.Books?.Select(b => new BookMapper().ToDto(b)).ToList()
        };
    }
}