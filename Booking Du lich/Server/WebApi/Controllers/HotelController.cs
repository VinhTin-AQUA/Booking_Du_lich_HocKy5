using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApi.DTOs.Hotel;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IAuthenRepository authenRepository;

        public HotelController(IHotelRepository hotelRepository, IAuthenRepository authenRepository)
        {
            this.hotelRepository = hotelRepository;
            this.authenRepository = authenRepository;
        }

        [HttpPost("add-hotel")]
        public async Task<IActionResult> AddHotel(AddHotel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var hotel = new Hotel
            {
                HotelName = model.HotelName,
            };
            var result = await hotelRepository.AddHotel(hotel);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add hotel" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add hotel successfully" }));
        }

        [HttpGet("get-all-hotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var cities = await hotelRepository.GetAllHotels();
            return Ok(cities);
        }

        [HttpGet("get-hotel-by-id")]
        public async Task<IActionResult> GetHotelById([FromQuery] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var hotel = await hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", mesage = "Hotel not found" }));
            }



            return Ok(hotel);
        }

        [HttpPost("add-agent")]
        public async Task<IActionResult> AddAgent(AddAgent model)
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
            if (hotel == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Hotel was not found" }));
            }

            var emailExist = await authenRepository.GetUserByEmail(model.Email);

            if (emailExist != null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Email already taken by other user. Please choose another email" }));
            }

            var newAgent = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = "",
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                HotelId = model.HotelId,
                Hotel = hotel,
            };
            var r = await hotelRepository.AdddAgent(newAgent, model.Password);
            if (r.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error. Please try again" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add agent successfully" }));
        }

        [HttpDelete("delete-agent")]
        public async Task<IActionResult> DeleteAgent([FromQuery]string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var agent = await authenRepository.GetUserById(id); 
            if(agent == null )
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Agent was not found" }));
            }

            var r = await hotelRepository.DeleteAgent(agent);

            if(r.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Some thing error. Please try again" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Delete agent successfully" }));
        }

    }
}
