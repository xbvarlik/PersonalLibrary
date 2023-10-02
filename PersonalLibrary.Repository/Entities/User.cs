using Microsoft.AspNetCore.Identity;

namespace PersonalLibrary.Repository.Entities;

public class User : IdentityUser<int>
{
    public virtual ICollection<BooksOfUser>? BooksOfUsers { get; set; }
}