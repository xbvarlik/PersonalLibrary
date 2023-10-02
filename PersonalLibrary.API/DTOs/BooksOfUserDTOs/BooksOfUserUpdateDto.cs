using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BooksOfUserDTOs;

public class BooksOfUserUpdateDto : IUpdateDto
{
    public string? UserId { get; set; }

    public int? BookId { get; set; }

    public int? StatusId { get; set; }

    public int? TagsOfUserId { get; set; }
}