using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class AuthorService : BaseService<Author, AuthorCreateDto ,AuthorReadDto, AuthorUpdateDto>
{
    public AuthorService(AppDbContext context, AuthorMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Author>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().Include(x => x.Books).ToListAsync();
    }

    public override async Task<Author> GetByIdAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}