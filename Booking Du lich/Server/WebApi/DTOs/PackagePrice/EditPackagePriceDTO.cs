using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.DTOs.PackagePrice
{
    public class EditPackagePriceDTO
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public int PackageID { get; set; }

        public DateTime ValidFrom { get; set; }
    }
}
