namespace Booking.Models
{
    public class TourType
    {
        public int TourId { get; set; }
        public int CategoryId { get; set; }
        public Tour? Tour { get; set; }
        public Category? Category { get; set; }
    }
}
