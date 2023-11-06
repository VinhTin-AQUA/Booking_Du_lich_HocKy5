using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> AddHotel([FromForm]List<IFormFile> files, [FromForm] AddHotel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var poster = await authenRepository.GetUserById(model.PosterID);
            var City = await cityRepository.GetCityById(model.CityId);

            var hotel = new Hotel
            {
                HotelName = model.HotelName,
                PosterID = model.PosterID,
                Poster = poster,
                Address = model.Address,
                Description = model.Description,
                PostingDate = DateTime.Now,
                ApprovalDate = null,
                CityId = model.CityId,
                City = City,
                ApproverID = null,
                Approver = null,
            };
            var result = await hotelRepository.AddHotel(hotel);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add hotel" }));
            }

            // lưu hình ảnh
            string urlImgFolder = "";
            if (files != null)
            {
                urlImgFolder = await imageService.UploadImages(files, hotel);
                hotel.PhotoPath = urlImgFolder;
            }

            var r = await hotelRepository.UpdateHotel(hotel);
            if (r == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add hotel successfully" , newHotel = hotel }));
        }

        [HttpPut("update-hotel")]
        public async Task<IActionResult> UpdateHotel([FromForm] List<IFormFile> files,[FromForm] UpdateHotel model)
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
            hotel.PosterID = model.PosterID;
            hotel.ApproverID = model.ApproverID;

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

        [HttpGet("hotels-not-approved")]
        public async Task<IActionResult> HotelNotApproved()
        {
            var hotels = await hotelRepository.HotelNotApproved();
            return Ok(hotels.ToList());
        }

        [HttpGet("hotels-approved")]
        public async Task<IActionResult> HotelApproved()
        {
            var hotels = await hotelRepository.HotelApproved();
            return Ok(hotels.ToList());
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
            var imgNames = imageService.GetAllFileOfFolder("hotels",hotel.Id.ToString(),"_imgHotel");

            return Ok(new { hotel, imgNames });
        }

        [HttpGet("get-hotels-of-agent")]
        public async Task<IActionResult> GetHotelOfAgent([FromQuery] string posterId)
        {
            if (string.IsNullOrEmpty(posterId))
            {
                return BadRequest();
            }
            var hotels = await hotelRepository.GetHotelsOfAgent(posterId);
            return Ok(new { hotels = hotels });
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
            imageService.DeleteAllImgHotel(hotel.Id.ToString());
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
        public IActionResult DeleteAllImgHotel([FromQuery] int hotelId)
        {
            imageService.DeleteAllImgHotel(hotelId.ToString());
            return Ok();
        }

        [HttpGet("get-hotels-in-city")]
        public async Task<IActionResult> GetCitiesInHotel([FromQuery] int cityId)
        {
            var hotels = await hotelRepository.GetHotelsInCity(cityId);
            LinkedList<string> fileNames = new LinkedList<string>();

            foreach (var hotel in hotels)
            {
                string[] imgNames = imageService.GetAllFileOfFolder("hotels",hotel.Id.ToString(), "_imgHotel");
                if (imgNames != null)
                {
                    fileNames.AddLast(imgNames[0]);
                } else
                {
                    fileNames.AddLast("");
                }
            }

            return Ok(new { hotels, fileNames });
        }

        [HttpPut("approve")]
        public async Task<IActionResult> Approve([FromQuery]string userId, [FromQuery]int hotelId)
        {
            var user = await authenRepository.GetUserById(userId);
            var hotel = await hotelRepository.GetHotelById(hotelId);

            if(user == null || hotel == null)
            {
                return BadRequest();
            }

            hotel.ApprovalDate = DateTime.Now;
            hotel.Approver = user;
            hotel.ApproverID = user.Id;
            var r = await hotelRepository.UpdateHotel(hotel);

            if (r == false)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
