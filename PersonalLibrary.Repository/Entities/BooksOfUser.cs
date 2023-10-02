using Microsoft.EntityFrameworkCore;

namespace PersonalLibrary.Repository.Entities;

public class BooksOfUser : BaseEntity
{
    public int UserId { get; set; } 

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public int TagsOfUserId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual User? User { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Status? Status { get; set; }

    public virtual Book? Book { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual TagsOfUser? TagsOfUser { get; set; }
}