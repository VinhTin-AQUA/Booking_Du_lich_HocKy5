using Booking.Areas.AgentHotel.Models.Hotel;
using Booking.Configs;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Services;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;

namespace Booking.Areas.AgentHotel.Controllers
{
	[Area("AgentHotel")]
	[Route("hotel-manager")]
	public class HotelController : Controller
	{
		private readonly ICityRepository cityRepository;
		private readonly IAuthenRepository authenRepository;
		private readonly IHotelRepository hotelRepository;
		private readonly IImageService imageService;
		private readonly AppConfigs appConfigs;

		public HotelController(ICityRepository cityRepository, IAuthenRepository authenRepository,
			IHotelRepository hotelRepository, IImageService imageService,
			AppConfigs appConfigs)
		{
			this.cityRepository = cityRepository;
			this.authenRepository = authenRepository;
			this.hotelRepository = hotelRepository;
			this.imageService = imageService;
			this.appConfigs = appConfigs;
		}

		public async Task<IActionResult> Index()
		{
			var hotels = await hotelRepository.GetAllHotels();
			List<string> listImgs = new List<string>();

			foreach (var hotel in hotels)
			{
				var img = imageService.GetFirstImage("hotels", hotel.Id.ToString(), "_imgHotel");
				listImgs.Add(img);
			}

			ViewBag.Hotel = hotels.ToList();
			ViewBag.Imgs = listImgs;
			ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
			return View();
		}

		[Route("add-hotel")]
		public async Task<IActionResult> AddHotel()
		{
			var cities = await cityRepository.GetAllCities();
			var citySelect = new SelectList(cities.ToList(), "Id", "Name");
			ViewBag.citySelect = citySelect;

			return View();
		}

		[Route("add-hotel")]
		[HttpPost]
		public async Task<IActionResult> AddHotel([FromForm] List<IFormFile> files, AddHotel model)
		{
			if (model == null)
			{
				return RedirectToAction("AddHotel");
			}

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var poster = await authenRepository.GetUserSignedIn(User);
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
				return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra. Vui lòng thử lại.");
			}

			// lưu hình ảnh
			string urlImgFolder = "";
			if (files != null)
			{
				urlImgFolder = await imageService.UploadImages(files, "hotels", hotel.Id.ToString(), "_imgHotel");
				hotel.PhotoPath = urlImgFolder;
			}

			var r = await hotelRepository.UpdateHotel(hotel);
			if (r == false)
			{
				return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại sau.");
			}

			return RedirectToAction("Index");
		}

		[Route("update-hotel")]
		public async Task<IActionResult> UpdateHotel(int hotelId)
		{
			var hotel = await hotelRepository.GetHotelById(hotelId);

			if(hotel == null)
			{
				return RedirectToAction("Index");
			}

			var cities = await cityRepository.GetAllCities();
			var citySelect = new SelectList(cities.ToList(), "Id", "Name");

			var imgHotels = imageService.GetAllFileOfFolder("hotels", hotel.Id.ToString(), "_imgHotel");

			ViewBag.citySelect = citySelect;
			ViewBag.imgHotels = imgHotels;
			ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
			return View(hotel);
		}

		[Route("update-hotel")]
		[HttpPost]
		public async Task<IActionResult> UpdateHotel([FromForm] List<IFormFile> files, Hotel model)
		{
			if (model == null)
			{
				return BadRequest();
			}

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var hotel = await hotelRepository.GetHotelById(model.Id);

			if (hotel == null)
			{
				return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra. Vui lòng thử lại.");
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
				urlImgFolder = await imageService.UploadImages(files, "hotels", hotel.Id.ToString(), "_imgHotel");
				hotel.PhotoPath = urlImgFolder;
			}

			var r = await hotelRepository.UpdateHotel(hotel);
			if (r == false)
			{
				return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại.");
			}

			return RedirectToAction("UpdateHotel", new { hotelId = hotel.Id });
		}

		[Route("delete-all-img-hotel")]
		public async Task<IActionResult> DeleteAllImageHotel(int hotelId)
		{
			var rImg = imageService.DeleteAllImages("hotels",hotelId.ToString(), "_imgHotel");

			if(rImg == false)
			{
                return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại.");
            }
			var hotel = await hotelRepository.GetHotelById(hotelId);
			hotel.PhotoPath = "/no-image.jpg";
			var r = await hotelRepository.UpdateHotel(hotel) ;
			if (r == false)
			{
                return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại.");
            }

            return RedirectToAction("UpdateHotel", new { hotelId = hotelId });
		}


        [HttpGet("delete-hotel")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var hotel = await hotelRepository.GetHotelById(hotelId);

            if (hotel == null)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại.");
            }

            var r = await hotelRepository.DeleteHotel(hotel);
            if (r == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi khi lưu hình ảnh. Vui lòng thử lại.");
            }
            imageService.DeleteAllImgHotel(hotel.Id.ToString());

            return RedirectToAction("Index");
        }

    }
}
