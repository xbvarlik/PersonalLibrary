using PersonalLibrary.Core.DTOs.Base;
using PersonalLibrary.Core.DTOs.BookDTOs;
using PersonalLibrary.Core.DTOs.StatusDTOs;
using PersonalLibrary.Core.DTOs.TagsOfUserDTOs;
using PersonalLibrary.Core.DTOs.UserDTOs;
using PersonalLibrary.Core.Entities;

namespace PersonalLibrary.Core.DTOs.BooksOfUserDTOs;

public class BooksOfUserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public int UserId { get; set; }

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public int TagsOfUserId { get; set; }

    public virtual UserReadDto? User { get; set; }

    public virtual StatusReadDto? Status { get; set; }

    public virtual BookReadDto? Book { get; set; }

    public virtual TagsOfUserReadDto? TagsOfUser { get; set; }
}