using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApi.DTOs
{
    public class CityDto
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "City Code")]
        public int CityCode { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "City name")]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
    }
}
