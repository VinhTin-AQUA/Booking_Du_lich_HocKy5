using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Booking.Models
{
    [Table("PackagePrice")]
    public class PackagePrice
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Key]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GoodThru { get; set; }

        [Key]
        public int PackageId { get; set; }

        [AllowNull]
        public Package? Package { get; set; }
    }
}
