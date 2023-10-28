using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.TourType
{
    public class EditTourTypeDto
    {
        public int TourTypeId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tour type name")]
        [Column(TypeName = "nvarchar(100)")]
        public string TourTypeName { get; set; }
    }
}
