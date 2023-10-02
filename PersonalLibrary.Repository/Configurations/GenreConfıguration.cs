using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class GenreConfıguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasData(new List<Genre>()
        {
            new ()
            {
                Id = 1,
                Name = "Epic Fantasy"
            },
            new ()
            {
                Id = 2,
                Name = "Gothic Horror",
            },
            new ()
            {
                Id = 3,
                Name = "Science Fiction",
            },
            new ()
            {
                Id = 4,
                Name = "Fantasy",
            },
            new ()
            {
                Id = 5,
                Name = "Dystopia",
            },
        });
    }
}