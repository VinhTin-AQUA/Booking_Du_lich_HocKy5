using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("TouristAttraction")]
    public class TouristAttraction
    {
        [Key]
        public int TouristAttractionId { get; set; }
        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(500)")]
        [AllowNull]
        public string Address {  get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tourist Attraction name")]
        [Column(TypeName = "nvarchar(500)")]
        public string TouristAttractionName { get; set; }
        // Tham chiếu khóa ngoại CityId
        public int? CityId { get; set; }
        public City? City { get; set; }
    }
}
