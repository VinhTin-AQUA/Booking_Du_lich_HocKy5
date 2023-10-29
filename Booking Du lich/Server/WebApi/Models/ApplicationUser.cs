using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        public BookRoom BookRoom { get; set; }

        public ICollection<Hotel> PostHotels { get;set; }
        public ICollection<Hotel> ApprovalHotels { get;set; }

        public ICollection<Tour> PostTours { get; set; }
        public ICollection<Tour> ApprovalTours { get; set; }

    }
}
