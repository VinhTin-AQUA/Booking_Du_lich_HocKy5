using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Room;
using WebApi.DTOs.RoomType;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeRepository roomTypeRepository;
       

        public RoomTypeController(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
            
        }

        [HttpPost("add-room-type")]
        public async Task<IActionResult> AddRoomType([FromForm] AddRoomTypeDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var roomType = await roomTypeRepository.GetRoomTypeByName(model.RoomTypeName);

            

            if (roomType != null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room Type đã tồn tại" }));
            }

            var newRoomType = new RoomType
            {
               RoomTypeName = model.RoomTypeName,
            };

            var result = await roomTypeRepository.AddRoomType(newRoomType);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add room type" }));
            }

            

            return Ok(new JsonResult(new { title = "Success", message = "Add room type successfully",RoomType = newRoomType }));
        }

        [HttpGet("get-all-room-types")]
        public async Task<IActionResult> GetAllRoomType()
        {
            var roomTypes = await roomTypeRepository.GetAllRoomTypes();
            
            return Ok(new JsonResult(new {title="Success", RoomTypes = roomTypes}));
        }

        [HttpGet("get-room-types-by-name")]
        public async Task<IActionResult> GetRoomTypeByName(string name)
        {
            var roomType = await roomTypeRepository.GetRoomTypeByName(name);
            if(roomType == null)
            {
                return NotFound(new JsonResult(new {title = "Error", message = "Room Type isn't exist"}));
            }

            return Ok(new JsonResult(new { title = "Success", RoomType = roomType }));
        }

        [HttpDelete("delete-room-type")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int? roomTypeId)
        {
            if (roomTypeId == null)
            {
                return BadRequest(new JsonResult(new {title="Error", message = "Missing parameter"}));
            }
            var roomType = await roomTypeRepository.GetRoomTypeById(roomTypeId);

            if (roomType == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room type was not found" }));
            }

            var result = await roomTypeRepository.DeleteRoomType(roomType);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Room type delete successfully" }));
        }

        [HttpGet("search-room-type")]
        public async Task<IActionResult> SearchRoomType(string roomTypeName)
        {
            if(roomTypeName == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var roomTypes = await roomTypeRepository.SearchRoomType(roomTypeName);
            
            return Ok(new JsonResult(new { title = "Success", RoomTypes = roomTypes }));
        }

        [HttpPut("update-room-type")]
        public async Task<IActionResult> UpdateRoomType( [FromForm] EditRoomTypeDTO model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room" }));
            }

            var roomExisted = await roomTypeRepository.GetRoomTypeById(model.RoomTypeId);
            if (roomExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Room type was not existed" }));
            }

            roomExisted.RoomTypeName = model.RoomTypeName;

            var resultUpdate = await roomTypeRepository.UpdateRoomType(roomExisted);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update room type" }));
            }

            
            return Ok(new JsonResult(new { title = "Success", message = "Update room type successfully" }));
        }
    }
}
