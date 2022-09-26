using Microsoft.AspNetCore.Identity;

namespace EmptyProject.Models
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
    }
}
