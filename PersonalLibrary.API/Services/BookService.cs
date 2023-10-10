using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Core.DTOs.BookDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Utilities.Managers;

namespace PersonalLibrary.API.Services;

public class BookService : BaseService<Book, BookCreateDto, BookReadDto, BookUpdateDto, BookQueryFilterDto>
{
    
    public BookService(AppDbContext context, BookMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Book>> GetAllAsync(BookQueryFilterDto query)
    {
        var data = _dbSet.AsQueryable();
        data = QuerySpecification(query, data);
        
        return await data
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .Include(x => x.Publisher)
            .ToListAsync();
    }

    public override async Task<Book> GetByIdAsync(int id)
    {
        var entity = await _dbSet
            .Include(x => x.Author)
            .Include(x => x.Genre)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) throw new NotFoundException();
        
        return entity;
    }

    public override async Task<BookReadDto> CreateAsync(BookCreateDto dto)
    {
        var entity = _mapper.ToEntity(dto);

        var image = ImageManager.SaveImageToLocalStorage(dto.CoverImage, Constants.Constants.DefaultImagePath);

        if (image != null) entity.CoverImage = image;
        
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.ToDto(entity, false);
    }

    public override async Task UpdateAsync(int id, BookUpdateDto dto)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) throw new KeyNotFoundException($"Entity not found with id {id}");
        
        entity = _mapper.ToEntity(dto, entity);
        
        var image = ImageManager.SaveImageToLocalStorage(dto.CoverImage, Constants.Constants.DefaultImagePath);

        if (image != null) entity.CoverImage = image;
        
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}