using Booking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("BookTour")]
    public class BookTour
    {
        [Key]
        public string UserID { get; set; }

        [Key]
        public int PackageId { get; set; }

        [Key]
        public DateTime? DepartureDate { get; set; }
       
        public DateTime? BookingDate { get; set; }

        // khoa ngoai

        public Package? Package { get; set; }

        public AppUser? User { get; set; }

        public double Price { get; set; }
    }
}
