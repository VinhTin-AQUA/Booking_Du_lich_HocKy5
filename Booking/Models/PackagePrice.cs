using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Booking.Models
{
    [Table("PackagePrice")]
    public class PackagePrice
    {
        [Key]
        public int PriceId { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Adult Price")]
        public double AdultPrice { get; set; }
		[Display(Name = "Child Price")]
		public double ChildPrice { get; set; }
		[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GoodThru { get; set; }
        public int PackageId { get; set; }

        [AllowNull]
        public Package? Package { get; set; }
    }
}
