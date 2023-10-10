using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.API.Core.BooksOfUserDTOs;

public class BooksOfUserCreateDto : ICreateDto
{
    public int UserId { get; set; } 

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public int TagsOfUserId { get; set; }
}