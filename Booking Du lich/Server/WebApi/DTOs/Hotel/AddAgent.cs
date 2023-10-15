using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Hotel
{
    public class AddAgent
    {
        public int HotelId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "{0} is invalid")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
