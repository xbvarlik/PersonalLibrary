using PersonalLibrary.API.DTOs.StatusDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class StatusMapper : BaseMapper<Status, StatusCreateDto, StatusReadDto, StatusUpdateDto>
{
    public override Status ToEntity(StatusCreateDto dto)
    {
        return new Status
        {
            Description = dto.Description
        };
    }

    public override Status ToEntity(StatusUpdateDto dto, Status entity)
    {
        entity.Description = dto.Description ?? entity.Description;

        return entity;
    }

    public override StatusReadDto ToDto(Status entity)
    {
        return new StatusReadDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Books = entity.Books?.Select(b => new BooksOfUserMapper().ToDto(b)).ToList()
        };
    }
}