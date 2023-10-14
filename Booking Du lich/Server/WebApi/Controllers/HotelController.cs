using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApi.DTOs.Hotel;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IAuthenRepository authenRepository;
        private readonly IEmailSender emailSender;

        public HotelController(IHotelRepository hotelRepository, IAuthenRepository authenRepository,
            IEmailSender emailSender)
        {
            this.hotelRepository = hotelRepository;
            this.authenRepository = authenRepository;
            this.emailSender = emailSender;
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

            string message = @$"<p>Hello <b>{newAgent.FirstName} {newAgent.LastName}</b></p> <p>Your passwork: <b>abc@123</b></p><p>Please change your password after confirm email</p>";

            if (await emailSender.SendEmailConfirmAsync(newAgent, message))
            {
                return Ok(new JsonResult(new
                {
                    title = "Success",
                    message = $"User created successfully and Send email to {newAgent.Email}"
                }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add agent successfully" }));
        }

    }
}
