using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Phone Number")]
        [Column(TypeName = "nvarchar(250)")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0} is at least {2} and max length is {1} characters")]
        public string Password { get; set; } = null!;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
