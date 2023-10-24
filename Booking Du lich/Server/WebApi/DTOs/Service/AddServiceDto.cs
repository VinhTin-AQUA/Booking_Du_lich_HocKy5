using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Service
{
    public class AddServiceDto
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Service name")]
        [Column(TypeName = "nvarchar(100)")]
        public string ServiceName { get; set; }
    }
}
