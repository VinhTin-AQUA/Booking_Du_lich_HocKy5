using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.ManageProfile.Models.Profile
{
    public class EditProfile
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
