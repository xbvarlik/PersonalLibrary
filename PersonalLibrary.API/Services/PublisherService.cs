﻿using Microsoft.EntityFrameworkCore;
using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class PublisherService : BaseService<Publisher, PublisherCreateDto, PublisherReadDto, PublisherUpdateDto, PublisherQueryFilterDto>
{
    public PublisherService(AppDbContext context, PublisherMapper mapper) : base(context, mapper) { }

    public override async Task<ICollection<Publisher>> GetAllAsync(PublisherQueryFilterDto query)
    {
        var data = _dbSet.AsQueryable();
        data = QuerySpecification(query, data);
        
        return await data.AsNoTracking().Include(x => x.Books).ToListAsync();
    }

    public override async Task<Publisher> GetByIdAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null) throw new NotFoundException();

        return entity;
    }
}