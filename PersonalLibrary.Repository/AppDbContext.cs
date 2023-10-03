using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Repository.Entities;

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

    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public override int SaveChanges()
    {
        ChangeTracker.Entries().ToList().ForEach(e =>
        {
            if (e.Entity is BaseEntity b)
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        b.CreatedAt = DateTime.Now;
                        b.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        b.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        e.State = EntityState.Modified;
                        b.IsDeleted = true;
                        break;
                }
            }
        });
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.Entries().ToList().ForEach(e =>
        {
            if (e.Entity is not BaseEntity b) return;
            
            switch (e.State)
            {
                case EntityState.Added:
                    b.CreatedAt = DateTime.Now;
                    b.UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    b.UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Deleted:
                    e.State = EntityState.Modified;
                    b.IsDeleted = true;
                    break;
            }
        });
        
        return base.SaveChangesAsync(cancellationToken);
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