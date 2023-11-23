using Booking.Areas.AgentHotel.Models.Room;
using Booking.Areas.AgentHotel.Models.RoomPrice;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.AgentHotel.Controllers
{
    [Area("AgentHotel")]
    [Route("room-price")]
    public class RoomPriceController : Controller
    {
        private readonly IRoomPriceRepository roomPriceRepository;
		private readonly IRoomRepository roomRepository;

		public RoomPriceController(IRoomPriceRepository roomPriceRepository,
            IRoomRepository roomRepository)
        {
            this.roomPriceRepository = roomPriceRepository;
			this.roomRepository = roomRepository;
		}

        public async Task<IActionResult> Index(int roomId)
        {
            var prices = await roomPriceRepository.GetRoomPricesOfRoom(roomId);
            ViewBag.Prices = prices.ToList();
            ViewBag.RoomId = roomId;
            return View();
        }

        [Route("add-room-price")]
        public IActionResult AddRoomPrice(int roomId)
        {
            ViewBag.RoomId = roomId;
            return View();
        }

        [Route("add-room-price")]
        [HttpPost]
        public async Task<IActionResult> AddRoomPrice(int roomId, AddRoomPrice model)
        {
            if (model == null)
            {
                return RedirectToAction("Index", new { roomId = roomId });
            }

            if(ModelState.IsValid == false)
            {
				return RedirectToAction("AddRoomPrice", new { roomId = roomId });
			}

            var room = await roomRepository.GetRoomById(roomId);

            if(room == null)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra. Vui lòng thử lại");
            }

			var roomPrice = new RoomPrice
            {
                Price = model.Price,
                ValidFrom = model.ValidFrom,
                GoodThru = model.GoodThru,
                RoomId = roomId,
                Room = room
            };

            var r = await roomPriceRepository.AddRoomPrice(roomPrice);
            if(r == false)
            {
				return RedirectToAction("Error", "Error", (object)"Có lỗi khi thêm giá phòng. Vui lòng thử lại");
			}

			return RedirectToAction("Index", new { roomId = roomId });
		}


        [Route("update-room-price")]
        public async Task<IActionResult> UpdateRoomPrice(int roomId, DateTime validFrom)
        {
            var roomPrice = await roomPriceRepository.GetRoomPriceByID(roomId, validFrom);
			ViewBag.RoomId = roomId;
			return View(roomPrice);
        }

        //[Route("update-room-price")]
        //public async Task<IActionResult> UpdateRoomPrice(int roomId, DateTime validFrom)
        //{
        //	var roomPrice = await roomPriceRepository.GetRoomPriceByID(roomId, validFrom);
        //	return View(roomPrice);
        //}

        [Route("delete-room-price")]
        public async Task<IActionResult> DeleteRoomPrice(int roomId, DateTime validFrom)
        {
			var roomPrice = await roomPriceRepository.GetRoomPriceByID(roomId, validFrom);
            var r = await roomPriceRepository.DeleteRoomPrice(roomPrice);   
            if(r == false)
            {
                return RedirectToAction("UpdateRoomPrice", new { roomId = roomId, validFrom = validFrom });
            }
            return RedirectToAction("Index", new { roomId = roomId });
		}

          
    }
}
