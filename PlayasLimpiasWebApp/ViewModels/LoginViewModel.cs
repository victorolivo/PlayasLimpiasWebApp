using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Please enter a user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }


    }
}
