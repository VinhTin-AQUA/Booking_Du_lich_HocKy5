using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.RoomPrice;
using WebApi.DTOs.RoomType;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomPriceController : ControllerBase
    {
        private readonly IRoomPriceRepository roomPriceRepository;
        private readonly IRoomRepository roomRepository;

        public RoomPriceController(IRoomPriceRepository roomPriceRepository, IRoomRepository roomRepository)
        {
            this.roomPriceRepository = roomPriceRepository;
            this.roomRepository = roomRepository;
        }
         
        [HttpPost("add-room-price")]
        public async Task<IActionResult> AddRoomPrice([FromForm] AddRoomPriceDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }



            var room = await roomRepository.GetRoomById(model.RoomId);

            var newRoomPrice = new RoomPrice
            {
                Price = model.Price,
                RoomId = model.RoomId,
                ValidFrom = DateTime.Now,
                Room = room
            };

            var result = await roomPriceRepository.AddRoomPrice(newRoomPrice);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add room price" }));
            }



            return Ok(new JsonResult(new { title = "Success", message = "Add room price successfully", RoomPrice = newRoomPrice }));
        }
         

        [HttpGet("get-all-room-price")]
        public async Task<IActionResult> GetAllPrices()
        {
            var roomPrices = await roomPriceRepository.GetAllRoomPrices();

            return Ok(new JsonResult(new { title = "Success", RoomPrice = roomPrices }));
        }

        [HttpGet("get-room-price-by-price")]
        public async Task<IActionResult> GetRoomTypeByName(decimal price)
        {
            var roomPrices = await roomPriceRepository.GetRoomPriceByPrice(price);
            if (roomPrices == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Room price isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", RoomPrice = roomPrices }));
        }

        [HttpGet("get-room-price-by-id")]
        public async Task<IActionResult> GetRoomTypeByName(int roomId, DateTime validFrom)
        {
            var roomPrices = await roomPriceRepository.GetRoomPriceByID(roomId,validFrom );
            if (roomPrices == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Room price isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", RoomPrice = roomPrices }));
        }


        [HttpDelete("delete-room-price")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int? roomId, [FromQuery] DateTime? validFrom)
        {
            if (roomId == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var roomPrice = await roomPriceRepository.GetRoomPriceByID(roomId, validFrom);

            if (roomPrice == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room price was not found" }));
            }

            var result = await roomPriceRepository.DeleteRoomPrice(roomPrice);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Room price delete successfully" }));
        }

        [HttpPut("update-room-price")]
        public async Task<IActionResult> UpdateRoomPrice([FromForm] EditRoomPriceDTO model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room price" }));
            }

            var roomPriceExisted = await roomPriceRepository.GetRoomPriceByID(model.RoomId, model.validFrom);
            if (roomPriceExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room type was not existed" }));
            }

            roomPriceExisted.Price = model.Price;

            var resultUpdate = await roomPriceRepository.UpdateRoomPrice(roomPriceExisted);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room price" }));
            }


            return Ok(new JsonResult(new { title = "Success", message = "Update room price successfully" }));
        }

       

    }
}
