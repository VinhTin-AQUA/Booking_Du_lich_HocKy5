using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Admin.Models.AgentHotelManager
{
    public class AddBusiness
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên đối tác")]
        public string PartnerName { get; set; }


        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }


        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Điện thoại đối tác")]
        [Phone(ErrorMessage = "{0} không hợp lệ")]
        public string PhoneNumber { get; set; }
    }
}
