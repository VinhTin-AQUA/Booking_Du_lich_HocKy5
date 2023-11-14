using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.AgentHotel.Controllers
{
    [Area("AgentHotel")]
    [Route("room-type")]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypeController(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
        }

        public async Task<IActionResult> Index(string searchString = "")
        {
            if(string.IsNullOrEmpty(searchString))
            {
				var _roomTypes = await roomTypeRepository.GetAllRoomTypes();
				ViewBag.RoomTypes = _roomTypes;
				return View();
			}

			var roomTypes = await roomTypeRepository.SearchRoomType(searchString);
			ViewBag.RoomTypes = roomTypes;
			return View();
		}

        [Route("add-room-type")]
        public IActionResult AddRoomType()
        {
            return View();
        }

        [Route("add-room-type")]
        [HttpPost]
        public async Task<IActionResult> AddRoomType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                ViewBag.err = "Tên loại phòng không được để trống";
				return View((object)typeName);
            }

            var typeExist = await roomTypeRepository.GetRoomTypeByName(typeName);

            if (typeExist != null)
            {
				ViewBag.err = "Tên loại phòng đã tồn tại";
				return View((object)typeName);
			}

            var roomType = new RoomType
            {
                RoomTypeName = typeName,
            };

            var r = await roomTypeRepository.AddRoomType(roomType);

            if(r == false)
            {
				ViewBag.err = "Có lỗi xảy ra. Vui lòng thử lại.";
				return View((object)typeName);
			}

            return RedirectToAction("Index");
        }

    }
}
