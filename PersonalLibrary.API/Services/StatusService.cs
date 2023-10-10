using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Core.DTOs.StatusDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;

namespace PersonalLibrary.API.Services;

public class StatusService : BaseService<Status, StatusCreateDto, StatusReadDto, StatusUpdateDto, StatusQueryFilterDto>
{
    public StatusService(AppDbContext context, StatusMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Status>> GetAllAsync(StatusQueryFilterDto query)
    {
        var data = _dbSet.AsQueryable();
        data = QuerySpecification(query, data);
        
        return await data.Include(x => x.BooksOfUsers).ToListAsync();
    }

    public override async Task<Status> GetByIdAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.BooksOfUsers).FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}