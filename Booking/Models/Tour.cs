using Booking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("Tour")]
    public class Tour
    {
        public int TourId { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tour name")]
        [Column(TypeName = "nvarchar(100)")]
        public string TourName { get; set;}

        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        public string TourAddress {  get; set;}

        [Display(Name = "Overview")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Overview { get; set;}

        [Display(Name = "Schedule")]
        [Column(TypeName = "nvarchar(max)")]
        [AllowNull]
        public string Schedule {  get; set;}

        [Display(Name = "Departure Location")]
        [Column(TypeName = "nvarchar(200)")]
        public string? DepartureLocation {  get; set;}

        [Display(Name = "Drop Off Location")]
        [Column(TypeName = "nvarchar(200)")]
        [AllowNull]
        public string DropOffLocation { get; set;}

        [Required(ErrorMessage = "{0} must be at least one image")]
        [Display(Name = "Images")]
        [Column(TypeName = "nvarchar(70)")]
        public string? PhotoPath { get; set;}

        // Khoa ngoai
            // city
        public int? CityId { get; set;}
        public City City { get; set;}
            // TourType
        public int? TourTypeId { get; set;}
        public Category? TourType { get; set;}
            // User
        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? PosterID { get; set; }
        public string? ApproverID { get; set; }
        public AppUser? Poster { get; set; }
        public AppUser? Approver { get; set; }

        public ICollection<Package>? Packages { get; set; }

        public ICollection<Visiting> Visitings { get; set; }
        public ICollection<TourType> TourTypes { get; set; }
    }
}
