using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    [Table("Service")]
    public class HotelService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Service name")]
        [Column(TypeName = "nvarchar(100)")]
        public string ServiceName { get; set; }
    }
}
