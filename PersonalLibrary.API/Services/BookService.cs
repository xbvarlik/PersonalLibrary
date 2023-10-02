using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class BookService : BaseService<Book, BookCreateDto, BookReadDto, BookUpdateDto>
{
    
    public BookService(AppDbContext context, BookMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Book>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking()
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
}