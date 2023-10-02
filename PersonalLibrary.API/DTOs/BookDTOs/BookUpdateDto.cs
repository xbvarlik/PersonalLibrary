using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BookDTOs;

public class BookUpdateDto : IUpdateDto
{
    public string? Title { get; set; } 

    public DateTime? PublishDate { get; set; }

    public int? NumberOfPages { get; set; }

    public int? Edition { get; set; }

    public string? CoverImage { get; set; }

    public int? Isbn { get; set; }

    public int? AuthorId { get; set; }

    public int? PublisherId { get; set; }

    public int? GenreId { get; set; }
}