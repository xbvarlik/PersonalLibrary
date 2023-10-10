using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.Core.BooksOfUserDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Core.DTOs.BooksOfUserDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;

namespace PersonalLibrary.API.Services;

public class BooksOfUserService : BaseService<BooksOfUser, BooksOfUserCreateDto, BooksOfUserReadDto, BooksOfUserUpdateDto, BooksOfUserQueryFilterDto>
{
    public BooksOfUserService(AppDbContext context, BooksOfUserMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<BooksOfUser>> GetAllAsync(BooksOfUserQueryFilterDto query)
    {
        var data = _dbSet.AsQueryable();
        data = QuerySpecification(query, data);
        
        return await data
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.TagsOfUser)
            .Include(x => x.Book)
            .ThenInclude(y => y.Author)
            .Include(x => x.Book)
            .ThenInclude(y => y.Genre)
            .Include(x => x.Book)
            .ThenInclude(y => y.Publisher)
            .ToListAsync();
    }

    public override async Task<BooksOfUser> GetByIdAsync(int id)
    {
        var entity = await _dbSet.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.TagsOfUser)
            .Include(x => x.Book)
            .ThenInclude(y => y.Author)
            .Include(x => x.Book)
            .ThenInclude(y => y.Genre)
            .Include(x => x.Book)
            .ThenInclude(y => y.Publisher)
            .FirstOrDefaultAsync();
        
        if (entity == null) throw new NotFoundException();
        
        return entity;
    }
}