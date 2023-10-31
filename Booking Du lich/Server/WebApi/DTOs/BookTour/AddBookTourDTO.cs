using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.BookTour
{
    public class AddBookTourDTO
    {
        [Required(ErrorMessage = "{0} must be required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? DepartureDate { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? BookingDate { get; set; }

        
    }
}
