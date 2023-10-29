using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.DTOs.Tour
{
    public class UpdateTourDto
    {
        public int TourId { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tour name")]
        public string TourName { get; set; }
        [Display(Name = "Address")]
        public string? TourAddress { get; set; }
        [Display(Name = "Overview")]
        public string? Overview { get; set; }
        [Display(Name = "Schedule")]
        public string? Schedule { get; set; }
        [Display(Name = "Departure Location")]
        public string? DepartureLocation { get; set; }
        [Display(Name = "Drop Off Location")]
        public string? DropOffLocation { get; set; }
        public string? PhotoPath { get; set; }
        public int CityId { get; set; }
        public int TourTypeId { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string PosterID { get; set; }
        public string ApproverID { get; set; }
    }
}
