namespace PersonalLibrary.Repository.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int NumberOfPages { get; set; }

    public int Edition { get; set; }

    public string? CoverImage { get; set; }

    public string Description { get; set; } = null!;
    
    public long Isbn { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int GenreId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual Genre? Genre { get; set; }
}