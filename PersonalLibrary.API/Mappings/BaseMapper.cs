using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public abstract class BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : ICreateDto
    where TReadDto : IReadDto
    where TUpdateDto : IUpdateDto
{
    protected virtual List<string>? NavigationProperties { get; set; }
    public abstract TEntity ToEntity(TCreateDto dto);
    public abstract TEntity ToEntity(TUpdateDto dto, TEntity entity);
    public abstract TReadDto ToDto(TEntity entity, bool includeNavigationProperties);
    protected abstract TReadDto MapOtherProperties(TEntity entity);
}