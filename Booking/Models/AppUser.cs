using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên")]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Địa chỉ")]
        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }

        public BookRoom? BookRoom { get; set; }

        public BookTour? BookTour { get; set; }

        public ICollection<Hotel>? PostHotels { get; set; }
        public ICollection<Hotel>? ApprovalHotels { get; set; }
        public int? PartnerId { get; set; }
        public BusinessPartner? BusinessPartner { get; set; }

        public ICollection<Tour>? PostTours { get; set; }
        public ICollection<Tour>? ApprovalTours { get; set; }
    }
}
