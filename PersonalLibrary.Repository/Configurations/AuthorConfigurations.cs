using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData(new List<Author>()
        {
            new ()
            {
                Id = 1,
                Name = "J.R.R. Tolkien",
            },
            new ()
            {
                Id = 2,
                Name = "Frank Herbert",
            },
            new ()
            {
                Id = 3,
                Name = "Bram Stoker",
            },
            new ()
            {
                Id = 4,
                Name = "Andrej Sapkovski",
            },
            new ()
            {
                Id = 5,
                Name = "George Orwell",
            },
            new ()
            {
                Id = 6,
                Name = "J.K. Rowling",
            },
        });
    }
}