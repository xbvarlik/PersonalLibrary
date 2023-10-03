using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.Repository.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "The Lord of the Rings",
                AuthorId = 1,
                GenreId = 1,
                PublisherId = 1,
                Description = "The Lord of the Rings is an epic high fantasy novel by the English author and scholar J. R. R. Tolkien.",
                PublishDate = new DateTime(2022, 3,29),  
                NumberOfPages = 1016,                    
                Edition = 16,                           
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9780395489321,
            },
            new Book()
            {
                Id = 2,
                Title = "The Hobbit",
                AuthorId = 1,
                GenreId = 1,
                PublisherId = 1,
                Description = "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien.",
                PublishDate = new DateTime(2015, 2, 20),  
                NumberOfPages = 425,                    
                Edition = 8,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9789752733732,
            },
            new Book()
            {
                Id = 3,
                Title = "Harry Potter and the Philosopher's Stone",
                AuthorId = 6,
                GenreId = 4,
                PublisherId = 1,
                Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling.",
                PublishDate = new DateTime(2017, 7, 29),  
                NumberOfPages = 450,                    
                Edition = 1,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9780395489321,
            },
            new Book()
            {
                Id = 4,
                Title = "Dune",
                AuthorId = 2,
                GenreId = 3,
                PublisherId = 1,
                Description = "Dune is a 1965 science-fiction novel by American author Frank Herbert, originally published as two separate serials in Analog magazine.",
                PublishDate = new DateTime(2021, 8, 15), 
                NumberOfPages = 707,                    
                Edition = 16,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9786053754794,
            },
            new Book()
            {
                Id = 5,
                Title = "The Last Wish",
                AuthorId = 4,
                GenreId = 1,
                PublisherId = 2,
                Description = "The Last Wish is the first book in Andrzej Sapkowski's Witcher series in terms of story chronology, although the original Polish edition was published in 1993, after Sword of Destiny.",
                PublishDate = new DateTime(2020, 12, 9),  
                NumberOfPages = 432,                    
                Edition = 4,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9786052291954,
            },
            new Book()
            {
                Id = 6,
                Title = "Dracula",
                AuthorId = 3,
                GenreId = 2,
                PublisherId = 1,
                Description = "Dracula is an 1897 Gothic horror novel by Irish author Bram Stoker.",
                PublishDate = new DateTime(2021, 6, 29),  
                NumberOfPages = 422,                    
                Edition = 3,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9786053758303,
            },
            new Book()
            {
                Id = 7,
                Title = "1984",
                AuthorId = 5,                
                GenreId = 5,
                PublisherId = 1,
                Description = "Nineteen Eighty-Four: A Novel, often referred to as 1984, is a dystopian social science fiction novel by English novelist George Orwell.",
                PublishDate = new DateTime(2021, 1, 12),  
                NumberOfPages = 324,                    
                Edition = 1,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9786257737753,
            },
            new Book()
            {
                Id = 8,
                Title = "Animal Farm",
                AuthorId = 5,
                GenreId = 5,
                PublisherId = 1,
                Description = "Animal Farm is an allegorical novella by George Orwell, first published in England on 17 August 1945.",
                PublishDate = new DateTime(2021, 1, 16),  
                NumberOfPages = 131,                    
                Edition = 1,                            
                CoverImage = "lord_of_the_rings.jpg",   
                Isbn = 9786257737746,
            },
        });  
    }
}