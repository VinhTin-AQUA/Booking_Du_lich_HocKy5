using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("TourType")]
    public class TourType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourTypeId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tour type name")]
        [Column(TypeName = "nvarchar(100)")]
        public string TourTypeName { get; set; }
    }
}
