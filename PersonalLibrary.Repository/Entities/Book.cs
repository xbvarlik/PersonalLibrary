namespace PersonalLibrary.Repository.Entities;

public class Book : BaseEntity
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

    public virtual Author? Author { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual Genre? Genre { get; set; }
}