using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Utilities.Accessors;

namespace PersonalLibrary.Repository;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Status> Status { get; set; } = null!;
    public DbSet<TagsOfUser> TagsOfUsers { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<BooksOfUser> BooksOfUsers { get; set; } = null!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = null!;

    private readonly ISessionAccessor _sessionAccessor;
    public AppDbContext(ISessionAccessor sessionAccessor)
    {
        _sessionAccessor = sessionAccessor;
    }

    public AppDbContext(DbContextOptions options, ISessionAccessor sessionAccessor) : base(options)
    {
        _sessionAccessor = sessionAccessor;
    }


    public override int SaveChanges()
    {
        var userId = _sessionAccessor.AccessUserId();
        ChangeTracker.Entries().ToList().ForEach(e =>
        {
            if (e.Entity is BaseEntity b)
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        b.CreatedAt = DateTime.Now;
                        b.UpdatedAt = DateTime.Now;
                        b.CreatedBy = userId;
                        b.UpdatedBy = userId;
                        break;
                    case EntityState.Modified:
                        b.UpdatedAt = DateTime.Now;
                        b.UpdatedBy = userId;
                        break;
                    case EntityState.Deleted:
                        e.State = EntityState.Modified;
                        b.DeletedAt = DateTime.Now;
                        b.DeletedBy = userId;
                        b.IsDeleted = true;
                        break;
                }
            }
        });
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = await _sessionAccessor.AccessUserIdAsync();
        ChangeTracker.Entries().ToList().ForEach(e =>
        {
            if (e.Entity is BaseEntity b)
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        b.CreatedAt = DateTime.Now;
                        b.UpdatedAt = DateTime.Now;
                        b.CreatedBy = userId;
                        b.UpdatedBy = userId;
                        break;
                    case EntityState.Modified:
                        b.UpdatedAt = DateTime.Now;
                        b.UpdatedBy = userId;
                        break;
                    case EntityState.Deleted:
                        e.State = EntityState.Modified;
                        b.DeletedAt = DateTime.Now;
                        b.DeletedBy = userId;
                        b.IsDeleted = true;
                        break;
                }
            }
        });
        
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Global query filtering for soft delete
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var isDeletedProperty = entityType.FindProperty("IsDeleted");
            
            if (isDeletedProperty == null || isDeletedProperty.ClrType != typeof(bool)) continue;
            
            var parameter = Expression.Parameter(entityType.ClrType);
            var prop = Expression.PropertyOrField(parameter, "IsDeleted");
            var filter = Expression.Lambda(Expression.Not(prop), parameter);
            
            builder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }
        
        // for seed data
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(builder);
    }
}