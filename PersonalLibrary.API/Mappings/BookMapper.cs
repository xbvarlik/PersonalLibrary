using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Mappings;

public class BookMapper : BaseMapper<Book, BookCreateDto, BookReadDto, BookUpdateDto>
{
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
            CoverImage = dto.CoverImage
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
        entity.CoverImage = dto.CoverImage ?? entity.CoverImage;

        return entity;
    }

    public override BookReadDto ToDto(Book entity)
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
            CoverImage = entity.CoverImage,
            Author = entity.Author is null ? null : new AuthorMapper().ToDto(entity.Author),
            Genre = entity.Genre is null ? null : new GenreMapper().ToDto(entity.Genre),
            Publisher = entity.Publisher is null ? null : new PublisherMapper().ToDto(entity.Publisher)
        };
    }
}