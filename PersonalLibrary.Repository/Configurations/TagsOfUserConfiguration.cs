using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class TagsOfUserConfiguration : IEntityTypeConfiguration<TagsOfUser>
{
    public void Configure(EntityTypeBuilder<TagsOfUser> builder)
    {
        builder.HasData(new List<Author>()
        {
            new ()
            {
                Id = 1,
                Name = "Default",
            },
            new ()
            {
                Id = 2,
                Name = "Favorites",
            }
        });
    }
}