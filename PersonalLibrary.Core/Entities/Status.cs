namespace PersonalLibrary.Core.Entities;

public class Status : BaseEntity
{
    public string Description { get; set; } = null!;
    public virtual ICollection<BooksOfUser>? BooksOfUsers { get; set; } = null!;
}