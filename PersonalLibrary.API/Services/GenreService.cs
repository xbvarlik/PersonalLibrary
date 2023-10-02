using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.GenreDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class GenreService : BaseService<Genre, GenreCreateDto, GenreReadDto, GenreUpdateDto>
{
    public GenreService(AppDbContext context, GenreMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Genre>> GetAllAsync()
    {
        return await _dbSet.Include(x => x.Books).AsNoTracking().ToListAsync();
    }

    public override async Task<Genre> GetByIdAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}