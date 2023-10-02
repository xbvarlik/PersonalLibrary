using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public abstract class BaseService<TEntity, TCreateDto, TReadDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : ICreateDto
    where TReadDto : IReadDto
    where TUpdateDto : IUpdateDto
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

    public abstract Task<ICollection<TEntity>> GetAllAsync();
    
    public abstract Task<TEntity> GetByIdAsync(Guid id);
    
    public virtual async Task<TReadDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.ToDto(entity);
    }
    
    public virtual async Task UpdateAsync(Guid id, TUpdateDto dto)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) throw new KeyNotFoundException($"Entity not found with id {id}");
        
        entity = _mapper.ToEntity(dto, entity);
        
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        
    }
    
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) throw new KeyNotFoundException($"Entity not found with id {id}");
        
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    
}