using PersonalLibrary.API.DTOs.StatusDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class StatusMapper : BaseMapper<Status, StatusCreateDto, StatusReadDto, StatusUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } = new() { nameof(Status.BooksOfUsers) };

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

    protected override StatusReadDto MapOtherProperties(Status entity)
    {
        return new StatusReadDto
        {
            Id = entity.Id,
            Description = entity.Description,
        };
    }
}