using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Authentication.Models.Authentication
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Required(ErrorMessage = "Email không được bỏ trống")]
        public string Email { get; set; } = null!;

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Số điện thoại")]
        [Column(TypeName = "nvarchar(250)")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0} phải ít nhất {2} ký tự và tối đa {1} ký tự")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Mật khẩu không chính xác")]
        [Required(ErrorMessage = "Mật khẩu không chính xác")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
