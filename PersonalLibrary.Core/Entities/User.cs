using Microsoft.AspNetCore.Identity;

namespace PersonalLibrary.Core.Entities;

public class User : IdentityUser<int>
{
    public virtual ICollection<BooksOfUser>? BooksOfUsers { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime UpdatedDate { get; set; }
    
    public bool IsDeleted { get; set; }
}