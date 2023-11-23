using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Authentication.Models.Authentication
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0} sai định dạng")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
