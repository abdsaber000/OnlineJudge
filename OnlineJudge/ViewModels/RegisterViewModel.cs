using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", 
            ErrorMessage = "Password doesn't match confirm password.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Handle { get; set; }



    }
}
