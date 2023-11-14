using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("City")]
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "City name")]
        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} must be at least one image")]
        [Display(Name = "Images")]
        [Column(TypeName = "nvarchar(70)")]
        public string? PhotoPath { get; set; }

        /*tham chiếu khóa ngoại*/
        public ICollection<Hotel>? Hotels { get; set;}
        public ICollection<Tour>? Tours { get; set;}
    }
}
