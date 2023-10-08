using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Authentication
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0} is at least {2} and max length is {1} characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="The password and confirm don't match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
