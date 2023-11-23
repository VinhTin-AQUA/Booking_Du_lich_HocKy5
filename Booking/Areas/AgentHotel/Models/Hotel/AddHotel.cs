using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking.Areas.AgentHotel.Models.Hotel
{
	public class AddHotel
	{
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Tên khách sạn")]
		public string HotelName { get; set; }

		[Display(Name = "Địa chỉ")]
		[Required(ErrorMessage = "{0} không được để trống")]
		public string? Address { get; set; }

		[Display(Name = "Mô tả")]
		[Required(ErrorMessage = "{0} không được để trống")]
		public string? Description { get; set; }


		public int CityId { get; set; }

		public string? PosterID { get; set; }
	}
}
