namespace PersonalLibrary.Repository.Entities;

public class TagsOfUser : BaseEntity
{
    public string TagName { get; set; } = null!;

    public string UserId { get; set; } = null!;
    
    public virtual User? User { get; set; }
    
    public virtual ICollection<BooksOfUser>? Books { get; set; } = null!;
}