using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("BookTourDetail")]
    public class BookTourDetail
    {
        [Key]
        public int TicketCode { get; set; }
        public string FirstNameTourist { get; set; }
        public string LastNameTourist { get;set; }
        public bool IsAdult { get; set; }

        // Khoa ngoai
        public int BookTourId { get; set; }

        [AllowNull]
        public BookTour? BookTour { get; set; }
    }
}
