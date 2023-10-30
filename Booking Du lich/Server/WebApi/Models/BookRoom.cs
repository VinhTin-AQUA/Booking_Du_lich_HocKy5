using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Book Room")]
    public class BookRoom
    {
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public string UserID { get; set; }
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public int RoomID { get; set; }
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? CheckInDate { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? CheckOutDate { get; set; }

        // khoa ngoai

        public Room Room { get; set; }  

        public ApplicationUser User { get; set; }

        public double Price { get; set; }
    }
}
