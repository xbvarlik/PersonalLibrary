namespace PersonalLibrary.Repository.Entities;

public class Status : BaseEntity
{
    public string Description { get; set; } = null!;
    public virtual ICollection<BooksOfUser>? Books { get; set; } = null!;
}