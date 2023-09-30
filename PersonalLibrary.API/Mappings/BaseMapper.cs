using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public abstract class BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : IBaseCreateDto
    where TReadDto : IBaseReadDto
    where TUpdateDto : IBaseUpdateDto
{
    public abstract TEntity ToEntity(TCreateDto dto);
    public abstract TEntity ToEntity(TUpdateDto dto, TEntity entity);
    public abstract TReadDto ToDto(TEntity entity);
}