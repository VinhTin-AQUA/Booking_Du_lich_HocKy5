using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Authentication
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
