using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.ManageProfile.Models.Profile
{
    public class ChangePassword
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
