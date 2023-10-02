using PersonalLibrary.API.DTOs.RoleDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class RoleMapper 
{
    public RoleReadDto MapToDto(Role entity)
    {
        return new RoleReadDto
        {
            Id = entity.Id,
            Name = entity.Name!
        };
    }
    
    public Role MapToEntity(RoleCreateDto dto)
    {
        return new Role
        {
            Name = dto.Name,
            NormalizedName = dto.Name.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
    }
    
    public Role MapToEntity(RoleUpdateDto dto, Role entity)
    {
        entity.Name = dto.Name ?? entity.Name;
        entity.NormalizedName = entity.Name?.ToUpper();

        return entity;
    }
}