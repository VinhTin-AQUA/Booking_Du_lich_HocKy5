using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class Visiting
    {
        public int TourId { get; set; }
        
        public int TouristAttractionId { get; set; }

        public Tour? Tour { get; set; }

        public TouristAttraction? TouristAttraction { get; set; }
    }
}
