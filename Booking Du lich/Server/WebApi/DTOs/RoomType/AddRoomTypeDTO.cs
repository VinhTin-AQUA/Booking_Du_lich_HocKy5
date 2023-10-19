using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.DTOs.RoomType
{
    public class AddRoomTypeDTO
    {

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Type Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string RoomTypeName { get; set; }
    }
}
