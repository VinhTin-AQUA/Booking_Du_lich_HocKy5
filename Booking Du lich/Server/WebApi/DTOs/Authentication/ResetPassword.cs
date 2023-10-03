using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Authentication
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="The password and confirm don't match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
