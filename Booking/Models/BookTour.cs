using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("BookTour")]
    public class BookTour
    {
        [Key]
        public int BookTourId { get; set; }
        public string UserID { get; set; }
        public int PackageId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? BookingDate { get; set; }
        public double? Price { get; set; }
        public string? SpecialRequirements { get; set; }

        // khoa ngoai

        public ICollection<BookTourDetail>? BookTourDetails { get; set; }

        public Package? Package { get; set; }

        public AppUser? User { get; set; }

    }
}
