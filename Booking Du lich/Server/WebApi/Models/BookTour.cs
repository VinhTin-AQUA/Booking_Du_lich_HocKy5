using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Book Tour")]
    public class BookTour
    {
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public string UserID { get; set; }
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public int PackageId { get; set; }
        [Key]
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? DepartureDate { get; set; }
       

        public DateTime? BookingDate { get; set; }

        // khoa ngoai

        public Package Package { get; set; }

        public ApplicationUser User { get; set; }

        public double Price { get; set; }
    }
}
