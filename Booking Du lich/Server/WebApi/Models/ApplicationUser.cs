using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

    }
}
