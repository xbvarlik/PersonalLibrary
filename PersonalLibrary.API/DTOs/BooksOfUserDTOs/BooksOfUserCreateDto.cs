using PersonalLibrary.API.DTOs.Base;

namespace PersonalLibrary.API.DTOs.BooksOfUserDTOs;

public class BooksOfUserCreateDto : ICreateDto
{
    public string UserId { get; set; } = null!;

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public int TagsOfUserId { get; set; }
}