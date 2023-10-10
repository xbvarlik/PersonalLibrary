using PersonalLibrary.Core.DTOs.Base;

namespace PersonalLibrary.Core.DTOs.BooksOfUserDTOs;

public class BooksOfUserUpdateDto : IUpdateDto
{
    public int? UserId { get; set; }

    public int? BookId { get; set; }

    public int? StatusId { get; set; }

    public int? TagsOfUserId { get; set; }
}