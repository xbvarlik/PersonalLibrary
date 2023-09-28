namespace PersonalLibrary.Repository.Entities;

public class Status : BaseEntity
{
    public string Description { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = null!;
}