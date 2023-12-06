using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("TourCategory")]
    public class TourCategory
    {
        public int TourId { get; set; }
        public int CategoryId { get; set; }
        public Tour? Tour { get; set; }
        public Category? Category { get; set; }
    }
}
