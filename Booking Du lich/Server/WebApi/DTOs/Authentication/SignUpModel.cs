using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Authentication
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0} is at least {2} and max length is {1} characters")]
        public string Password { get; set; } = null!;
    }
}
