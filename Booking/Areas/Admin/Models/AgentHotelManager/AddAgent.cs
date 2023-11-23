using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Admin.Models.AgentHotelManager
{
    public class AddAgent
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
        [Phone(ErrorMessage = "{0} sai định dạng")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        public string Email { get; set; }

        public string Password { get; set; } = "default123";
    }
}
