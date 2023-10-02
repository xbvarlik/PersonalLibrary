using Microsoft.AspNetCore.Identity;

namespace PersonalLibrary.Repository.Entities;

public class User : IdentityUser
{
    public virtual ICollection<BooksOfUser>? BooksOfUsers { get; set; }
}