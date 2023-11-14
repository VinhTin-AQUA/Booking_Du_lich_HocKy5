using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.Authentication.Models.Authentication
{
    public class ConfirmEmailModel
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        [EmailAddress(ErrorMessage = "{0} sai định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
