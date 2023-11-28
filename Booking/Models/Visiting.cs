using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class Visiting
    {
        public int TourId { get; set; }
        
        public int TouristAttractionId { get; set; }
    }
}
