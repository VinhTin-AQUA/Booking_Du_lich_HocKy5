using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Package
{
    public class AddPackageDTO
    {
       
        [Required(ErrorMessage = "{0} must be required")]
        public int TourID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "{0} must be required")]
        public string PackageName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Decription { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        public int MaxPeople { get; set; }
    }
}
