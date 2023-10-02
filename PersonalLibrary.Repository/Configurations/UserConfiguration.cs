using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        User superAdmin = new()
        {
            Id = 1,
            UserName = "SuperAdmin@nttdata.com",
            NormalizedUserName = "SUPERADMIN@NTTDATA.COM",
            Email = "SuperAdmin@nttdata.com",
            NormalizedEmail = "SUPERADMIN@NTTDATA.COM",
            EmailConfirmed = true,
            PhoneNumber = "",
            PhoneNumberConfirmed = true,
            FirstName = "Super Admin",
            LastName = "Super Admin",
            SecurityStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            TwoFactorEnabled = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            IsDeleted = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            AccessFailedCount = 0,
        };

        superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123NttData-_-");
        
        User admin = new()
        {
            Id = 2,
            UserName = "Admin@nttdata.com",
            NormalizedUserName = "ADMIN@NTTDATA.COM",
            Email = "Admin@nttdata.com",
            NormalizedEmail = "ADMIN@NTTDATA.COM",
            EmailConfirmed = true,
            PhoneNumber = "",
            PhoneNumberConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin",
            SecurityStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            TwoFactorEnabled = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            IsDeleted = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            AccessFailedCount = 0,
        };

        admin.PasswordHash = CreatePasswordHash(superAdmin, "123NttData-_-");
        
        User user = new()
        {
            Id = 3,
            UserName = "User@nttdata.com",
            NormalizedUserName = "USER@NTTDATA.COM",
            Email = "User@nttdata.com",
            NormalizedEmail = "USER@NTTDATA.COM",
            EmailConfirmed = true,
            PhoneNumber = "",
            PhoneNumberConfirmed = true,
            FirstName = "User",
            LastName = "User",
            SecurityStamp = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            TwoFactorEnabled = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            IsDeleted = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            AccessFailedCount = 0,
        };

        user.PasswordHash = CreatePasswordHash(superAdmin, "123NttData-_-");
        
        builder.HasData(superAdmin, admin, user);
    }
    private string CreatePasswordHash(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(user, password);
    }
    
}