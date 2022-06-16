using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.Models
{
    public class User : IdentityUser
    {
        //ID is inherited from the base class
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
    }
}
