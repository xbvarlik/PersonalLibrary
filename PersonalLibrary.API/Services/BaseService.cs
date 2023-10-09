using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public abstract class BaseService<TEntity, TCreateDto, TReadDto, TUpdateDto, TQueryFilterDto>
    where TEntity : BaseEntity
    where TCreateDto : ICreateDto
    where TReadDto : IReadDto
    where TUpdateDto : IUpdateDto
    where TQueryFilterDto : IQueryFilterDto
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto> _mapper;

    protected BaseService(AppDbContext context, BaseMapper<TEntity, TCreateDto, TReadDto, TUpdateDto> mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<TEntity>();
    }

    public abstract Task<ICollection<TEntity>> GetAllAsync(TQueryFilterDto query);
    
    public abstract Task<TEntity> GetByIdAsync(int id);
    
    public virtual async Task<TReadDto> CreateAsync(TCreateDto dto, int userId)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync(userId);
        return _mapper.ToDto(entity, false);
    }
    
    public virtual async Task UpdateAsync(int id, TUpdateDto dto, int userId)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) throw new KeyNotFoundException($"Entity not found with id {id}");
        
        entity = _mapper.ToEntity(dto, entity);
        
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(userId);
        
    }
    
    public virtual async Task DeleteAsync(int id, int userId)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) throw new KeyNotFoundException($"Entity not found with id {id}");
        
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(userId);
    }
    
    protected IQueryable<TEntity> QuerySpecification(TQueryFilterDto query, IQueryable<TEntity> queryableData)
    {
        var properties = typeof(TQueryFilterDto).GetProperties().ToList();
        foreach (var property in properties)
        {
            var value = property.GetValue(query);
                
            if (value != null)
            {
                queryableData = ApplyPropertyFilter(queryableData, property.Name, value);
            }
        }
            
        return queryableData;
    }
    
    private static IQueryable<TEntity> ApplyPropertyFilter(IQueryable<TEntity> queryable, string propertyName, object? value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var property = Expression.Property(parameter, propertyName);
        
        if (property.Type.IsGenericType && property.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(property.Type);
            var convertedValue = value != null ? Convert.ChangeType(value, underlyingType!) : null;

            var hasValueProperty = Expression.Property(property, "HasValue");
            var getValueProperty = Expression.Property(property, "Value");
            var equals = Expression.Equal(getValueProperty, Expression.Constant(convertedValue));
            
            var condition = Expression.AndAlso(hasValueProperty, equals);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

            return queryable.Where(lambda);
        }
        else
        {
            var equals = Expression.Equal(property, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return queryable.Where(lambda);
        }
    }
}