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
    protected abstract TReadDto MapOtherProperties(TEntity entity);
    public virtual TReadDto ToDto(TEntity entity, bool includeNavigationProperty)
    {
        var dto = MapOtherProperties(entity);
        
        var frame = new StackTrace().GetFrame(1);
        var callerName= frame?.GetMethod()?.DeclaringType?.Name ?? throw new NotFoundException("callerName is not found");

        if (!includeNavigationProperty) return dto;
        
        if (NavigationProperties == null) throw new NotFoundException("NavigationProperties is null.");
            
        if (callerName.Contains("Mapper")) callerName = callerName.Substring(0, callerName.Length - 6);

        if (NavigationProperties.Contains(callerName)) NavigationProperties.Remove(callerName);

        foreach (var prop in NavigationProperties)
        {
            var property = entity.GetType().GetProperty(prop);
            if (property == null) continue;
            var propertyType = property.GetType();
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            
            if (propertyType == typeof(ICollection<>)) propertyName = propertyName.Substring(0, propertyName.Length - 1);
            
            // take namespace as a property
            var mapperType = Type.GetType($"PersonalLibrary.API.Mappings.{propertyName}Mapper");
            if(mapperType == null) throw new NotFoundException("Mapper not found.");

            var mapperInstance = Activator.CreateInstance(mapperType);
            var toDtoMethod = mapperType.GetMethod("ToDto");
            if (toDtoMethod == null) throw new NotFoundException("ToDto method not found.");
                
            object? navigationProperty = null;
            if (propertyValue != null)
                navigationProperty = toDtoMethod.Invoke(mapperInstance, new object[] { propertyValue, false }); 
                
            dto.GetType().GetProperty(prop)?.SetValue(dto, navigationProperty);
        }
        return dto;
    }
    
}