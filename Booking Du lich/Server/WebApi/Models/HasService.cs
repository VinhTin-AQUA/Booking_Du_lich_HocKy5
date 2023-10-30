
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WebApi.Models
{
    [Table("Has Service")]
    public class HasService
    {
        [Required(ErrorMessage = "{0} must be required")]
        public int HotelID { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public int ServiceID { get; set; }

        // Khoa ngoai
        public Hotel Hotel { get; set; }
        public HotelService Service { get; set; }
    }
}
