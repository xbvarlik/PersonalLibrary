using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData(new List<Status>()
        {
            new ()
            {
                Id = 1,
                Description = "To Read",
            },
            new ()
            {
                Id = 2,
                Description = "Reading",
            },
            new ()
            {
                Id = 3,
                Description = "Read",
            },
            new ()
            {
                Id = 4,
                Description = "On Loan",
            },
            new ()
            {
                Id = 5,
                Description = "Lost",
            },
        });
    }
}