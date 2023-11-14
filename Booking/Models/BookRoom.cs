using Booking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("BookRoom")]
    public class BookRoom
    {
        [Key]
        public string UserID { get; set; }

        [Key]
        public int RoomID { get; set; }

        [Key]
        public DateTime? CheckInDate { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? CheckOutDate { get; set; }

        public DateTime? BookingDate { get; set; }

        // khoa ngoai

        public Room? Room { get; set; }  

        public AppUser? User { get; set; }

        public double Price { get; set; }
    }
}
