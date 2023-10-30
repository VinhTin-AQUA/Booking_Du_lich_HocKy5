using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.DTOs.BusinessPartner
{
    public class AddBusinessPartner
    {
        [Required(ErrorMessage = "{0} must be require")]
        [Display(Name = "Business Partner")]
        [Column(TypeName = ("nvarchar(100)"))]
        public string PartnerName { get; set; }

        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(100)")]
        public string? Address { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Column(TypeName = "nvarchar(15)")]
        public string PhoneNumber { get; set; }

        
    }
}
