using Microsoft.AspNetCore.Identity;

namespace PlayasLimpiasWebApp.Models
{
    public class User :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
