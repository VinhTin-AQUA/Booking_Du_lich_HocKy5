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
