using Booking.Areas.AgentHotel.Models.Room;
using Booking.Configs;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Areas.AgentHotel.Controllers
{
    [Area("AgentHotel")]
    [Route("room-manager")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;
        private readonly IImageService imageService;
        private readonly AppConfigs appConfigs;
        private readonly IHotelRepository hotelRepository;
        private readonly IRoomPriceRepository roomPriceRepository;
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomController(IRoomRepository roomRepository,
            IImageService imageService,
            AppConfigs appConfigs,
            IHotelRepository hotelRepository,
            IRoomTypeRepository roomTypeRepository)
        {
            this.roomRepository = roomRepository;
            this.imageService = imageService;
            this.appConfigs = appConfigs;
            this.hotelRepository = hotelRepository;
            this.roomTypeRepository = roomTypeRepository;
        }

        public async Task<IActionResult> Index(int hotelId, string searchString = "")
        {
            var roomsOfHotel = await roomRepository.GetAllRoomsOfHotel(hotelId, searchString);
            List<string> imgRooms = new List<string>();

            foreach (var room in roomsOfHotel)
            {
                var imgRoom = imageService.GetFirstImage("hotels", hotelId.ToString(), room.Id.ToString());

                imgRooms.Add(imgRoom);
            }

            ViewBag.RoomsOfHotel = roomsOfHotel.ToList();
            ViewBag.ImgRooms = imgRooms;
            ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
            ViewBag.HotelId = hotelId;
            return View();
        }

        [Route("add-room")]
        public async Task<IActionResult> AddRoom(int hotelId)
        {
            var roomTypes = await roomTypeRepository.GetAllRoomTypes();
            ViewBag.HotelId = hotelId;
            ViewBag.roomTypes = new SelectList(roomTypes, "RoomTypeId", "RoomTypeName");
            return View();
        }

        [Route("add-room")]
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromForm] List<IFormFile> files, AddRoom model, int hotelId)
        {
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid == false)
            {
                return RedirectToAction("AddRoom", new { hotelId = hotelId });
            }

            var hotel = await hotelRepository.GetHotelById(hotelId);
            var roomType = await roomTypeRepository.GetRoomTypeById(model.RoomTypeId);

            if (hotel == null)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra. Vui lòng thử lại.");
            }

            var room = new Room
            {
                RoomNumber = model.RoomNumber,
                RoomName = model.Name,
                Description = model.Description,
                IsAvailable = model.IsAvailable,
                HotelId = hotelId,
                RoomTypeId = model.RoomTypeId,
                Hotel = hotel,
                RoomType = roomType
            };

            var result = await roomRepository.AddRoom(room);

            if (result == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra. Vui lòng thử lại.");
            }

            //var roomPrice = new RoomPrice
            //{
            //    Price = model.Price,
            //    ValidFrom = model.ValidFrom,
            //    GoodThru = model.GoodThru,
            //    RoomId = room.Id,
            //    Room = room,
            //};
            //await roomPriceRepository.AddRoomPrice(roomPrice);
            //room.RoomPrice = roomPrice;
            ////room.RoomId = roomType.RoomId;
            //room.ValidFrom = room.ValidFrom;


            string photoPath = "";
            if (files != null)
            {
                photoPath = await imageService.AddRoomImages(files, hotel, room);
            }
            room.PhotoPath = photoPath;
            await roomRepository.UpdateRoom(room);

            return RedirectToAction("Index", new { hotelId = hotel.Id });
        }

        [Route("update-room")]
        public async Task<IActionResult> UpdateRoom(int roomId)
        {
            var room = await roomRepository.GetRoomById(roomId);
            var roomTypes = await roomTypeRepository.GetAllRoomTypes();

            ViewBag.roomTypes = new SelectList(roomTypes, "RoomTypeId", "RoomTypeName");
            ViewBag.imgRooms = imageService.GetAllFileOfFolder("hotels",room.Hotel.Id.ToString(), room.Id.ToString());
            ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
            return View(room);
        }

        [Route("update-room")]
        [HttpPost]
        public async Task<IActionResult> UpdateRoom([FromForm] List<IFormFile> files, [Bind("Id,RoomNumber,RoomName,Description,IsAvailable,RoomTypeId")] UpdateRoom model)
        {
            if (model == null)
            {
                return RedirectToAction("Index","Hotel");
            }

            var roomExisted = await roomRepository.GetRoomById(model.Id);
            if (roomExisted == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy phòng. Vui lòng thử lại.");
            }

            roomExisted.RoomNumber = model.RoomNumber;
            roomExisted.RoomName = model.RoomName;
            roomExisted.Description = model.Description;
            roomExisted.IsAvailable = model.IsAvailable;

            var roomType = await roomTypeRepository.GetRoomTypeById(model.RoomTypeId);

            if (roomType != null)
            {
                roomExisted.RoomType = roomType;
                roomExisted.RoomTypeId = roomType.RoomTypeId;
            }

            //var price = await roomPriceRepository.GetRoomPriceById(model.Id);

            //if (price != null)
            //{
            //    price.Price = model.Price;
            //    price.ValidFrom = model.ValidFrom;
            //    price.GoodThru = model.GoodThru;
            //    await roomPriceRepository.UpdateRoomPrice(price);
            //}

            var resultUpdate = await roomRepository.UpdateRoom(roomExisted);
            if (resultUpdate == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi khi cập nhật. Vui lòng thử lại.");
            }

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadImages(files, "hotels",roomExisted.Hotel.Id.ToString(),roomExisted.Id.ToString());
                roomExisted.PhotoPath = urlImgFolder;
            }
            return RedirectToAction("UpdateRoom", new { roomId = roomExisted.Id});
        }



    }
}
