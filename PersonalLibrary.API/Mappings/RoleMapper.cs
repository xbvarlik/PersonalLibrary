using PersonalLibrary.Core.DTOs.RoleDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Mappings;

public class RoleMapper 
{
    public RoleReadDto ToDto(Role entity)
    {
        return new RoleReadDto
        {
            Id = entity.Id,
            Name = entity.Name!
        };
    }
    
    public Role ToEntity(RoleCreateDto dto)
    {
        return new Role
        {
            Name = dto.Name,
            NormalizedName = dto.Name.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
    }
    
    public Role ToEntity(RoleUpdateDto dto, Role entity)
    {
        entity.Name = dto.Name ?? entity.Name;
        entity.NormalizedName = entity.Name?.ToUpper();

        return entity;
    }
}