using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Booking.Models
{
    [Table("RoomPrice")]
    public class RoomPrice
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Key]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GoodThru { get; set; }

        [Key]
        public int RoomId { get; set; } 

        public Room Room { get; set; }
    }
}
