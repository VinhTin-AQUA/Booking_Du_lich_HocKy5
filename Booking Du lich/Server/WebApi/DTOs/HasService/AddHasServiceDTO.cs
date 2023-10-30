using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.HasService
{
    public class AddHasServiceDTO
    {
        [Required(ErrorMessage = "{0} must be required")]
        public int HotelID { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        public int ServiceID { get; set; }
    }
}
