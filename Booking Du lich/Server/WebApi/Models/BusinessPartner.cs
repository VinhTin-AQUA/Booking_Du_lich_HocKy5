using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("BusinessPartner")]
    public class BusinessPartner
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must be require")]
        [Display(Name ="Business Partner")]
        [Column(TypeName =("nvarchar(100)"))]
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

         // tham chiếu khóa ngoại
         public ICollection<ApplicationUser> PartnerUser { get; set; }



    }
}
