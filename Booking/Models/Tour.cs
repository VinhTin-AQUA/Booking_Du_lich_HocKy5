using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("Tour")]
    public class Tour
    {
        public int TourId { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên tour")]
        [Column(TypeName = "nvarchar(500)")]
        public string TourName { get; set; }

        [Display(Name = "Tổng quan")]
        [Column(TypeName = "nvarchar(max)")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string? Overview { get; set; }

        [Display(Name = "Lịch trình")]
        [Column(TypeName = "nvarchar(max)")]
        [AllowNull]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string Schedule { get; set; }

        [Display(Name = "Địa điểm đón khách")]
        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string? DepartureLocation { get; set; }

        [Display(Name = "Địa điểm trả khách")]
        [Column(TypeName = "nvarchar(200)")]
        [AllowNull]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public string DropOffLocation { get; set; }

        //[Required(ErrorMessage = "{0} ít nhất 1 ảnh")]
        [Display(Name = "Hình ảnh")]
        //[Column(TypeName = "nvarchar(70)")]
        public string? PhotoPath { get; set; }

        // Khoa ngoai

        // TourType

        public ICollection<TourCategory>? TourCategories { get; set; }

        // User
        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string? PosterID { get; set; }
        public AppUser? Poster { get; set; }

        public string? ApproverID { get; set; }
        public AppUser? Approver { get; set; }

        public ICollection<Package>? Packages { get; set; }

        //public ICollection<Visiting> Visitings { get; set; }
        public ICollection<CityTour>? CityTours { get; set; }

    }
}
