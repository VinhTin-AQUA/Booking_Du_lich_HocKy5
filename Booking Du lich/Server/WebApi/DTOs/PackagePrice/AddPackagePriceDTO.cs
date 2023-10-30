using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.DTOs.PackagePrice
{
    public class AddPackagePriceDTO
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public int PackageId { get; set; }
    }
}
