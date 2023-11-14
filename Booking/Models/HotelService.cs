using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("Service")]
    public class HotelService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Service name")]
        [Column(TypeName = "nvarchar(100)")]
        [AllowNull]
        public string ServiceName { get; set; }

        // KHoa ngoai
        public ICollection<HasService>? HasServices { get; set; }
    }
}
