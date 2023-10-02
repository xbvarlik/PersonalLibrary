using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.GenreDTOs;
using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.API.Mappings;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.DTOs.BookDTOs;

public class BookReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int NumberOfPages { get; set; }

    public int Edition { get; set; }

    public string CoverImage { get; set; } = null!;

    public long Isbn { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int GenreId { get; set; }

    public virtual AuthorReadDto? Author { get; set; }

    public virtual PublisherReadDto? Publisher { get; set; }

    public virtual GenreReadDto? Genre { get; set; }
}