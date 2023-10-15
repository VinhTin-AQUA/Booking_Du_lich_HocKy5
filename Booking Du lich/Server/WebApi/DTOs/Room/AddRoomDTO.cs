using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DTOs.Room
{
    public class AddRoomDTO
    {

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Number Room")]
        [Column(TypeName = "nvarchar(250)")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
    }
}
