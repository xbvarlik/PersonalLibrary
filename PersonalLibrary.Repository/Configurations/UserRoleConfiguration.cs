using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(new List<UserRole>()
        {
            new ()
            {
                UserId = 1,
                RoleId = 1
            },
            new ()
            {
                UserId = 2,
                RoleId = 2
            },
            new ()
            {
                UserId = 3,
                RoleId = 3
            },
        });
    }
}