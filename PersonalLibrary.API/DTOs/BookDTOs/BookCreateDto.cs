using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BookDTOs;

public class BookCreateDto : ICreateDto
{
    public string Title { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int NumberOfPages { get; set; }

    public int Edition { get; set; }

    public IFormFile? CoverImage { get; set; }

    public long Isbn { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int GenreId { get; set; }
}