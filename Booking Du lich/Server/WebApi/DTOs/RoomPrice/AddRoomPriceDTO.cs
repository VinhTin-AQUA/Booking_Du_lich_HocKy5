using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.DTOs.RoomPrice
{
    public class AddRoomPriceDTO
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public int RoomId { get; set; }
    }
}
