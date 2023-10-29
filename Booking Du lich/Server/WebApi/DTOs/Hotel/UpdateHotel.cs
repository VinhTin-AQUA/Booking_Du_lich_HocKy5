using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CityModel = WebApi.Models.City;

namespace WebApi.DTOs.Hotel
{
    public class UpdateHotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Hotel name")]
        public string HotelName { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public string? PhotoPath { get; set; }

        public int CityId { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string PosterID { get; set; }
        public string ApproverID { get; set; }
    }
}
