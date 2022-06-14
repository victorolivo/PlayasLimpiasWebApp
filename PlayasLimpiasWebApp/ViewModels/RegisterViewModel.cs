using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

    }
}
