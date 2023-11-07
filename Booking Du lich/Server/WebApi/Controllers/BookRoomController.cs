using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BookRoom;
using WebApi.DTOs.RoomType;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRoomController : ControllerBase
    {
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IAuthenRepository authenRepository;

        public BookRoomController(IBookRoomRepository bookRoomRepository, IRoomRepository roomRepository, IAuthenRepository authenRepository) { 
            this.bookRoomRepository = bookRoomRepository; 
            this.roomRepository = roomRepository;
            this.authenRepository = authenRepository;
        }
        [HttpPost("add-book-room")]
        public async Task<IActionResult> AddRoomType([FromForm]  AddBookRoomDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var bookRoom = await bookRoomRepository.GetBookRoomByID(model.UserID, model.RoomID, model.CheckInDate);


            if (bookRoom != null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Book Room đã tồn tại" }));
            }

            var Room = await roomRepository.GetRoomById(model.RoomID);
            var User = await authenRepository.GetUserById(model.UserID);

            var newBookRoom = new BookRoom
            {
                UserID = model.UserID,
                RoomID = model.RoomID,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                BookingDate = model.BookingDate,
                Room = Room,
                User = User,
            };

            var result = await bookRoomRepository.AddBookRoom(newBookRoom);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add book room " }));
            }



            return Ok(new JsonResult(new { title = "Success", message = "Add book room  successfully", BookRoom = newBookRoom }));
        }

        [HttpGet("get-all-book-rooms")]
        public async Task<IActionResult> GetAllBookRooms()
        {
            var bookRooms = await bookRoomRepository.GetAllBookRooms();

            return Ok(new JsonResult(new { title = "Success", BookRoom = bookRooms }));
        }

        [HttpGet("get-book-room-by-id")]
        public async Task<IActionResult> GetBookRoomByID(string userID, int roomID, DateTime? checkInDate)
        {
            var bookRoom = await bookRoomRepository.GetBookRoomByID(userID, roomID, checkInDate);
            if (bookRoom == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Book Room isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", BookRoom = bookRoom }));
        }

        [HttpDelete("delete-book-room")]
        public async Task<IActionResult> DeleteRoom([FromQuery] string userID, [FromQuery] int roomID, [FromQuery] DateTime? checkInDate)
        {
            if (userID == null || roomID == null || checkInDate == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var bookRoom = await bookRoomRepository.GetBookRoomByID(userID, roomID, checkInDate);

            if (bookRoom == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Book Room was not found" }));
            }

            var result = await bookRoomRepository.DeleteBookRoom(bookRoom);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Book Room delete successfully" }));
        }
 

       
    }
}
