using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class BooksOfUserConfiguration : IEntityTypeConfiguration<BooksOfUser>
{
    public void Configure(EntityTypeBuilder<BooksOfUser> builder)
    {
        builder.HasData(new List<BooksOfUser>()
        {
            new ()
            {
                Id = 1,
                UserId = 3,
                BookId = 1,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                Id = 2,
                UserId = 3,
                BookId = 2,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                Id = 3,
                UserId = 3,
                BookId = 3,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                Id = 4,
                UserId = 1,
                BookId = 4,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                Id = 5,
                UserId = 1,
                BookId = 5,
                StatusId = 2,
                TagsOfUserId = 1
            },
        });
    }
}