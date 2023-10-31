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

        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public int CityId { get; set; }
        public string CityCode { get; set; }

        public string PosterID
        {
            get; set;
        }
    }
}
