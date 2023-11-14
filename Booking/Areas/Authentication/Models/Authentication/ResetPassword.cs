using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Authentication.Models.Authentication
{
    public class ResetPassword
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Bạn đã nhập sai mật khẩu")]
        public string ConfirmPassword { get; set; }
    }
}
