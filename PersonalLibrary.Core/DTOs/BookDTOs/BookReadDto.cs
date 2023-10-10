using PersonalLibrary.Core.DTOs.GenreDTOs;
using PersonalLibrary.Core.DTOs.PublisherDTOs;
using PersonalLibrary.Core.DTOs.AuthorDTOs;
using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.BookDTOs;

public class BookReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int NumberOfPages { get; set; }

    public int Edition { get; set; }

    public byte[] CoverImage { get; set; } = null!;

    public long Isbn { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int GenreId { get; set; }

    public virtual AuthorReadDto? Author { get; set; }

    public virtual PublisherReadDto? Publisher { get; set; }

    public virtual GenreReadDto? Genre { get; set; }

}