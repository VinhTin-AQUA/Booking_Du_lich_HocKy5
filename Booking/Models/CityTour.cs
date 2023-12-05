using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("CityTour")]
    public class CityTour
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
