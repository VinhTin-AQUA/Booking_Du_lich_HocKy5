using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Hotel
{
    public class AddHotel
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Hotel name")]
        [Column(TypeName = "nvarchar(250)")]
        public string HotelName { get; set; }
    }
}
