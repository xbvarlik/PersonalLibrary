namespace PersonalLibrary.Repository.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Book>? Books { get; set; }
}