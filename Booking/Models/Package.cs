using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Booking.Models
{
    [Table("Package")]
    public class Package
    {
        [Required(ErrorMessage = "{0} must be required")]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public int TourID { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "{0} must be required")]
        public string PackageName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Decription { get; set; }
        public int MaxPeople { get; set; }

        //--
        public Tour Tour { get; set; }

        public ICollection<PackagePrice>? PackagePrices { get; set; }

        public ICollection<BookTour>? BookTours { get; set; }
    }
}
