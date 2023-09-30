using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BooksOfUserDTOs;

public class BooksOfUserCreateDto : ICreateDto
{
    public string UserId { get; set; } = null!;

    public Guid BookId { get; set; }

    public Guid StatusId { get; set; }

    public Guid TagsOfUserId { get; set; }
}