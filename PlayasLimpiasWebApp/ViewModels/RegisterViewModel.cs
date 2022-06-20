using System.ComponentModel.DataAnnotations;

namespace PlayasLimpiasWebApp.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {

        [Required(ErrorMessage = "Please enter a first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
