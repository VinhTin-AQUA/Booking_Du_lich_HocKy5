using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApi.DTOs.City
{
    public class EditCityDto
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "City name")]
        public string Name { get; set; }
    }
}
