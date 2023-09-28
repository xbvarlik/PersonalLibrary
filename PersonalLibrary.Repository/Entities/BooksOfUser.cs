using Microsoft.EntityFrameworkCore;

namespace PersonalLibrary.Repository.Entities;

public class BooksOfUser : BaseEntity
{
    public string UserId { get; set; } = null!;

    public Guid BookId { get; set; }

    public Guid StatusId { get; set; }

    public Guid TagsOfUserId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual User? User { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Status? Status { get; set; }

    public virtual Book? Book { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual TagsOfUser? TagsOfUser { get; set; }
}