using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.DTOs.Room
{
    public class EditRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Number Room")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; }

        public int HotelId { get; set; }
    }
}
