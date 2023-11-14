using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.AgentHotel.Models.RoomPrice
{
    public class AddRoomPrice
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Giá phòng")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} phải lớn hơn {1}")]
        public double Price { get; set; }

        [Display(Name = "Ngày áp dụng")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GoodThru { get; set; }
    }
}
