using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BooksOfUserDTOs;

public class BooksOfUserUpdateDto : IUpdateDto
{
    public string? UserId { get; set; }

    public Guid? BookId { get; set; }

    public Guid? StatusId { get; set; }

    public Guid? TagsOfUserId { get; set; }
}