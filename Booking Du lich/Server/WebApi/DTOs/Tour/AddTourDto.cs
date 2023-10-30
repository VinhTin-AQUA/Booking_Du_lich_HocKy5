using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Tour
{
    public class AddTourDto
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Tour name")]
        [Column(TypeName = "nvarchar(100)")]
        public string TourName { get; set; }
        public int? TourTypeId { get; set; }
        public int? CityId { get; set; }
        public string TourAddress { get; set; }
        public string Overview { get; set; }
        public string Schedule { get; set; }
        public string DepartureLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string PosterID { get; set; }
        public string ApproverID { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string PhotoPath { get; set; }
    }
}
