namespace PersonalLibrary.Repository.Entities;

public class TagsOfUser : BaseEntity
{
    public string TagName { get; set; } = null!;

    public int UserId { get; set; }
    
    public virtual User? User { get; set; }
    
    public virtual ICollection<BooksOfUser>? Books { get; set; } = null!;
}