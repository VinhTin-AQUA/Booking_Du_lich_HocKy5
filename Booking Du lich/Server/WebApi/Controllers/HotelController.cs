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
        private readonly ICityRepository cityRepository;
        private readonly IImageService imageService;

        public HotelController(IHotelRepository hotelRepository, IAuthenRepository authenRepository,
            IEmailSender emailSender, ICityRepository cityRepository, IImageService imageService)
        {
            this.hotelRepository = hotelRepository;
            this.authenRepository = authenRepository;
            this.emailSender = emailSender;
            this.cityRepository = cityRepository;
            this.imageService = imageService;
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

            return Ok(new JsonResult(new { title = "Success", message = "Add hotel successfully" , newHotel = hotel }));
        }

        [HttpPut("update-hotel")]
        public async Task<IActionResult> UpdateHotel(List<IFormFile> files,[FromForm] UpdateHotel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var hotel = await hotelRepository.GetHotelById(model.Id);

            if(hotel == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Hotel was not found" }));
            }

            var city = await cityRepository.GetCityById(model.CityId);

            hotel.HotelName = model.HotelName;
            hotel.Address = model.Address;
            hotel.Description = model.Description;
            hotel.CityId = model.CityId;
            hotel.City = city;

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadImages(files, hotel);
                hotel.PhotoPath = urlImgFolder;
            }

            var r = await hotelRepository.UpdateHotel(hotel);
            if(r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Update hotel successfully" }));
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

        [HttpGet("get-hotel-of-agent")]
        public async Task<IActionResult> GetHotelOfAgent([FromQuery] string agentId)
        {
            if (string.IsNullOrEmpty(agentId))
            {
                return BadRequest();
            }

            var hotel = await hotelRepository.GetHotelOfAgent(agentId);
            if (hotel == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Không tìm thấy khách sạn" }));
            }

            var imgFileNames = imageService.GetAllFileOfFolder("hotels", hotel.Id.ToString(), "_imgHotel");

            return Ok(new {hotel = hotel, imgFileNames = imgFileNames});
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

            await authenRepository.AddRoleToUser(newAgent, "Agent");

            string message = @$"<p>Hello <b>{newAgent.FirstName} {newAgent.LastName}</b></p> <p>Your passwork: <b>abc123</b></p><p>Please change your password after confirm email</p>";

            if (await emailSender.SendEmailConfirmAsync(newAgent, message))
            {
                return Ok(new JsonResult(new
                {
                    title = "Success",
                    message = $"User created successfully and Send email to {newAgent.Email}"
                }));
            }

            return BadRequest(new JsonResult(new { title = "Error", message = "Error when sent email confirmation to agent" }));
        }

        [HttpDelete("delete-hotel")]
        public async Task<IActionResult> DeleteHotel([FromQuery] int? hotelId)
        {
            if (hotelId == null)
            {
                return BadRequest();
            }
            var hotel = await hotelRepository.GetHotelById(hotelId);

            if (hotel == null)
            {
                return BadRequest(new JsonResult(new { title= "Error", message = "Hotel was not found" }));
            }

            var r = await hotelRepository.DeleteHotel(hotel);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Hotel delete successfully" }));
        }

        [HttpDelete("delete-img-hotel")]
        public IActionResult DeleteImgHotel([FromQuery]string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                return BadRequest();
            }
            imageService.DeleteImgHotel(url);

            return Ok();
        }

        [HttpDelete("delete-all-img-hotel")]
        public IActionResult DeleteAllImgHotel([FromQuery] string hotelId)
        {
            if (string.IsNullOrEmpty(hotelId))
            {
                return BadRequest();
            }
            imageService.DeleteAllImgHotel(hotelId);
            return Ok();
        }
    }
}
