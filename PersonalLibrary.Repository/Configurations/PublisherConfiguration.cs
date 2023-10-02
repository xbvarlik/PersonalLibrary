using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasData(new List<Publisher>()
        {
            new ()
            {
                Id = 1,
                Name = "İthaki Yayınları",
            },
            new ()
            {
                Id = 2,
                Name = "Laika Yayınları",
            }
        });
    }
}