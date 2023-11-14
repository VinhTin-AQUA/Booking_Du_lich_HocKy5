using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.AgentHotel.Models.Room
{
    public class AddRoom
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Số phòng")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Tên phòng")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string? Description { get; set; }

        [Display(Name = "Còn trống")]
        public bool IsAvailable { get; set; } = true; 

        public int RoomTypeId { get; set; }
    }
}
