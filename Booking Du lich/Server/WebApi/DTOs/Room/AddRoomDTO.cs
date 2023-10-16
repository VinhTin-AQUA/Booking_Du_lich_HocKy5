using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DTOs.Room
{
    public class AddRoomDTO
    {

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Number Room")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int HotelId { get; set; }
    }
}
