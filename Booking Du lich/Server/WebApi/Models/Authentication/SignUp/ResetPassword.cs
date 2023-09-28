using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Authentication.SignUp
{
    public class ResetPassword
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "The Password anf ConfirmPassword don't match")]
        public string confirmPassword { get; set; } = null!;
    }
}
