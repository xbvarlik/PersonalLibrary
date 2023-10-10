using PersonalLibrary.Core.DTOs.BookDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.API.Mappings;

public class BookMapper : BaseMapper<Book, BookCreateDto, BookReadDto, BookUpdateDto>
{
    protected override List<string>? NavigationProperties { get; set; } =
        new() { nameof(Book.Author), nameof(Book.Genre), nameof(Book.Publisher) };

    public override Book ToEntity(BookCreateDto dto)
    {
        return new Book
        {
            Title = dto.Title,
            AuthorId = dto.AuthorId,
            GenreId = dto.GenreId,
            Isbn = dto.Isbn,
            NumberOfPages = dto.NumberOfPages,
            PublisherId = dto.PublisherId,
            PublishDate = dto.PublishDate,
            Edition = dto.Edition,
        };
    }

    public override Book ToEntity(BookUpdateDto dto, Book entity)
    {
        entity.Title = dto.Title ?? entity.Title;
        entity.AuthorId = dto.AuthorId ?? entity.AuthorId;
        entity.GenreId = dto.GenreId ?? entity.GenreId;
        entity.Isbn = dto.Isbn ?? entity.Isbn;
        entity.NumberOfPages = dto.NumberOfPages ?? entity.NumberOfPages;
        entity.PublisherId = dto.PublisherId ?? entity.PublisherId;
        entity.PublishDate = dto.PublishDate ?? entity.PublishDate;
        entity.Edition = dto.Edition ?? entity.Edition;

        return entity;
    }

    protected override BookReadDto MapOtherProperties(Book entity)
    {
        return new BookReadDto
        {
            Id = entity.Id,
            Title = entity.Title,
            AuthorId = entity.AuthorId,
            GenreId = entity.GenreId,
            Isbn = entity.Isbn,
            NumberOfPages = entity.NumberOfPages,
            PublisherId = entity.PublisherId,
            PublishDate = entity.PublishDate,
            Edition = entity.Edition,
        };
    }
    
    public override BookReadDto ToDto(Book entity, bool includeNavigationProperties)
    {
        var dto = MapOtherProperties(entity);
        if (!includeNavigationProperties) return dto;
        
        dto.Author = new AuthorMapper().ToDto(entity.Author, false);
        dto.Genre = new GenreMapper().ToDto(entity.Genre, false);
        dto.Publisher = new PublisherMapper().ToDto(entity.Publisher, false);

        return dto;
    }
}