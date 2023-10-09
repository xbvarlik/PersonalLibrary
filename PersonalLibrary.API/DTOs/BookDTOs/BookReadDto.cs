using PersonalLibrary.API.DTOs.AuthorDTOs;
using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.GenreDTOs;
using PersonalLibrary.API.DTOs.PublisherDTOs;
using PersonalLibrary.API.Interfaces;
using PersonalLibrary.API.Mappings;

namespace PersonalLibrary.API.DTOs.BookDTOs;

public class BookReadDto : IReadDto, IExcludeNavigationProperty
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
    public void ExcludeNavigationProperty(string callerName)
    {
        switch (callerName)
        {
            case nameof(AuthorMapper):
                this.Author = null;
                break;
            case nameof(GenreMapper):
                this.Genre = null;
                break;
            case nameof(PublisherMapper):
                this.Publisher = null;
                break;
        }
    }
}