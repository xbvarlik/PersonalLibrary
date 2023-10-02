using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.TagsOfUserDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class TagsOfUserService : BaseService<TagsOfUser, TagsOfUserCreateDto, TagsOfUserReadDto, TagsOfUserUpdateDto>
{
    public TagsOfUserService(AppDbContext context, TagsOfUserMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<TagsOfUser>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Books)
            .ToListAsync();
    }

    public override async Task<TagsOfUser> GetByIdAsync(int id)
    {
        var entity = await _dbSet
            .Include(x => x.User)
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}