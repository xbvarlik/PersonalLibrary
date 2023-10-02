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
                UserId = 3,
                BookId = 1,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                UserId = 3,
                BookId = 2,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                UserId = 3,
                BookId = 3,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                UserId = 1,
                BookId = 4,
                StatusId = 2,
                TagsOfUserId = 1
            },
            new ()
            {
                UserId = 1,
                BookId = 5,
                StatusId = 2,
                TagsOfUserId = 1
            },
        });
    }
}