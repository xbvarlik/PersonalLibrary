using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BookDTOs;

public class BookCreateDto : ICreateDto
{
    public string Title { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int NumberOfPages { get; set; }

    public int Edition { get; set; }

    public string CoverImage { get; set; } = null!;

    public int Isbn { get; set; }

    public Guid AuthorId { get; set; }

    public Guid PublisherId { get; set; }

    public Guid GenreId { get; set; }
}