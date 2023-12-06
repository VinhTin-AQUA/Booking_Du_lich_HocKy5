using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("BusinessPartner")]
    public class BusinessPartner
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên đối tác")]
        [Column(TypeName = ("nvarchar(100)"))]
        [AllowNull]
        public string PartnerName { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Column(TypeName = "nvarchar(100)")]
        [AllowNull]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [EmailAddress(ErrorMessage = "{0} sai định dạng")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Column(TypeName = "nvarchar(15)")]
        [Phone(ErrorMessage = "{0} sai định dạng")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [AllowNull]
        public string PhoneNumber { get; set; }

        // tham chiếu khóa ngoại
        public ICollection<AppUser>? PartnerUser { get; set; }
    }
}
