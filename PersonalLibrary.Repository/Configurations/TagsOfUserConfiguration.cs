using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class TagsOfUserConfiguration : IEntityTypeConfiguration<TagsOfUser>
{
    public void Configure(EntityTypeBuilder<TagsOfUser> builder)
    {
        builder.HasData(new List<TagsOfUser>()
        {
            new ()
            {
                Id = 1,
                TagName = "Default",
                UserId = 1
            },
            new ()
            {
                Id = 2,
                TagName = "Favorites",
                UserId = 1
            },
            new ()
            {
                Id = 3,
                TagName = "Default",
                UserId = 2
            },
            new ()
            {
                Id = 4,
                TagName = "Favorites",
                UserId = 2
            },
            new ()
            {
                Id = 5,
                TagName = "Default",
                UserId = 3
            },
            new ()
            {
                Id = 6,
                TagName = "Favorites",
                UserId = 3
            }
        });
    }
}