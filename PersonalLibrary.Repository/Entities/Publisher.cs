namespace PersonalLibrary.Repository.Entities;

public class Publisher : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Book>? Books { get; set; }
}