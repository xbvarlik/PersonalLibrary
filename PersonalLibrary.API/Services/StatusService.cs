using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.StatusDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class StatusService : BaseService<Status, StatusCreateDto, StatusReadDto, StatusUpdateDto, StatusQueryFilterDto>
{
    public StatusService(AppDbContext context, StatusMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Status>> GetAllAsync(StatusQueryFilterDto query)
    {
        var data = _dbSet.AsQueryable();
        data = QuerySpecification(query, data);
        
        return await data.Include(x => x.Books).ToListAsync();
    }

    public override async Task<Status> GetByIdAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}