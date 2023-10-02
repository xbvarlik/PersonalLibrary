using PersonalLibrary.API.DTOs.Base;
using PersonalLibrary.API.DTOs.BookDTOs;
using PersonalLibrary.API.DTOs.StatusDTOs;
using PersonalLibrary.API.DTOs.TagsOfUserDTOs;
using PersonalLibrary.API.DTOs.UserDTOs;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.DTOs.BooksOfUserDTOs;

public class BooksOfUserReadDto : IReadDto
{
    public int Id { get; set; }
    
    public string UserId { get; set; } = null!;

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public int TagsOfUserId { get; set; }

    public virtual UserReadDto? User { get; set; }

    public virtual StatusReadDto? Status { get; set; }

    public virtual BookReadDto? Book { get; set; }

    public virtual TagsOfUserReadDto? TagsOfUser { get; set; }
}