using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.Models
{
    [Table("Room Type")]
    public class RoomType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomTypeId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Type Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string RoomTypeName { get; set; }

        // Khoa ngoai

        public ICollection<Room> Rooms { get; set; }
    }
}
