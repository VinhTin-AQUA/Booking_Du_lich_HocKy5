using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.City;
using WebApi.DTOs.Hotel;
using WebApi.DTOs.Room;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        private readonly IImageService imageService;
        private readonly IHotelRepository hotelRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IRoomPriceRepository roomPriceRepository;

        public RoomController(IRoomRepository roomRepository, IImageService imageService, IHotelRepository hotelRepository, IRoomTypeRepository roomTypeRepository, IRoomPriceRepository roomPriceRepository)
        {
            this.roomRepository = roomRepository;
            this.imageService = imageService;
            this.hotelRepository = hotelRepository;
            this.roomTypeRepository = roomTypeRepository;
            this.roomPriceRepository = roomPriceRepository;
        }

        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom([FromForm] List<IFormFile> files, [FromForm] AddRoomDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var hotel = await hotelRepository.GetHotelById(model.HotelId);
            var roomType = await roomTypeRepository.GetRoomTypeById(model.RoomTypeId);
            

            if (hotel == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Hotel không tìm thấy" }));
            }

            var room = new Room
            {
                RoomNumber = model.RoomNumber,
                RoomName = model.Name,
                Description = model.Description,
                IsAvailable = model.IsAvailable,
                HotelId = model.HotelId,
                RoomTypeId= model.RoomTypeId,
                Hotel = hotel,
                RoomType = roomType
            };
            
            var result = await roomRepository.AddRoom(room);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add hotel" }));
            }

            var roomPrice = new RoomPrice
            {
                Price = model.Price,
                ValidFrom = model.ValidFrom,
                GoodThru = model.GoodThru,
                RoomId = room.Id,
                Room = room,
            };
            await roomPriceRepository.AddRoomPrice(roomPrice);
            room.RoomPrice = roomPrice;
            //room.RoomId = roomType.RoomId;
            room.ValidFrom = room.ValidFrom;


            string photoPath = "";
            if (files != null)
            {
                photoPath = await imageService.AddRoomImages(files, hotel, room);
            }
            room.PhotoPath = photoPath;
            await roomRepository.UpdateRoom(room);

            return Ok(new JsonResult(new { title = "Success", message = "Add room successfully", newRoom = room }));
        }

        [HttpGet("get-room-by-id")]
        public async Task<IActionResult> GetRoomById([FromQuery]int roomId)
        {
            var room = await roomRepository.GetRoomById(roomId);
            if (room == null)
            {
                return BadRequest();
            }
            var imgNames = imageService.GetAllFileOfFolder("hotels", room.Hotel.Id.ToString(), room.Id.ToString());
            return Ok(new { room , imgNames });
        } 

        [HttpGet("get-all-rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await roomRepository.GetAllRooms();
            var list = rooms.ToList();
            int n = list.Count();
            string[] firstImages = new string[n];

            for (int i = 0; i < n; i++)
            {
                string firstImage = imageService.GetFirstImageOfRoom(list[i].PhotoPath);
                firstImages[i] = firstImage;
            }
            return Ok(new { rooms = rooms.ToList() , firstImages = firstImages });
        }

        [HttpGet("get-images-of-room")]
        public async Task<IActionResult> GetImagesOfRoom([FromQuery]int? roomId)
        {
            if(roomId == null)
            {
                return BadRequest();
            }
            var room = await roomRepository.GetRoomById(roomId);

            if (room == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room was not found" }));
            }

            var images = imageService.GetAllFileOfFolder("hotels",room.HotelId.ToString(), room.Id.ToString());
            return Ok(images);

        }

        [HttpDelete("delete-img-room")]
        public IActionResult DeleteImgHotel([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest();
            }
            imageService.DeleteImgHotel(url);

            return Ok();
        }

        [HttpDelete("delete-room")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int? roomId)
        {
            if (roomId == null)
            {
                return BadRequest();
            }
            var room = await roomRepository.GetRoomById(roomId);

            if (room == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room was not found" }));
            }

            var r = await roomRepository.DeleteRoom(room);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }

            imageService.DeleteAllImages("hotels",room.Hotel.Id.ToString(),room.Id.ToString());
            return Ok(new JsonResult(new { title = "Success", message = "Room delete successfully" }));
        }

        [HttpGet("search-room-available")]
        public async Task<IActionResult> SearchRoom(string available)
        {
            var check = true;
            if (String.Compare(available.ToLower(), "co san") == 0)
            {
                check = true;

            }
            else // logic chưa chặt
            {
                check = false;
            }

            var rooms = await roomRepository.SearchRoom(check);
            return Ok(new JsonResult(new { title = "Success", rooms }));
        }

        [HttpGet("search-rooms-of-hotel")]
        public async Task<IActionResult> SearchRoomsOfHotel(int? HotelId)
        {
           if(HotelId == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            
            var rooms = await roomRepository.SearchRoomOfHotel(HotelId);

            LinkedList<string> fileNames = new LinkedList<string>();

            foreach (var room in rooms)
            {
                string[] imgNames = imageService.GetAllFileOfFolder("hotels", room.Hotel.Id.ToString(), room.Id.ToString(), "_imgHotel");
                if (imgNames != null)
                {
                    fileNames.AddLast(imgNames[0]);
                }
                else
                {
                    fileNames.AddLast("");
                }
            }


            return Ok(new JsonResult(new { title = "Success", Rooms = rooms, fileNames = fileNames }));
        }

        [HttpPut("update-room")]
        public async Task<IActionResult> UpdateRoom(List<IFormFile> files, [FromForm] EditRoomDTO model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room" }));
            }

            var roomExisted = await roomRepository.GetRoomById(model.Id);
            if (roomExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room was not existed" }));
            }

            roomExisted.RoomNumber = model.RoomNumber;
            roomExisted.RoomName = model.Name;
            roomExisted.Description = model.Description;
            roomExisted.IsAvailable = model.IsAvailable;

            var resultUpdate = await roomRepository.UpdateRoom(roomExisted);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room" }));
            }

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadImages(files, roomExisted.Hotel, roomExisted);
                roomExisted.PhotoPath = urlImgFolder;
            }

            return Ok(new JsonResult(new { title = "Success", message = "Update room successfully" }));
        }
    }
}
