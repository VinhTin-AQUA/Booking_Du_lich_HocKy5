using Booking.Configs;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("city-manager")]
    public class CityController : Controller
    {
        private readonly AppConfigs appConfigs;
        private readonly ICityRepository cityRepository;
        private readonly IImageService imageService;

        public CityController(AppConfigs appConfigs, 
            ICityRepository cityRepository,
            IImageService imageService)
        {
            this.appConfigs = appConfigs;
            this.cityRepository = cityRepository;
            this.imageService = imageService;
        }

        public async Task<IActionResult> Index(string searchString = "")
        {
            var cities = await cityRepository.GetAllCities(searchString);
            ViewBag.Cities = cities;
            ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
            return View();
        }

        [Route("add-city")]
        public IActionResult AddCity()
        {
            return View();
        }

        [Route("add-city")]
        [HttpPost]
        public async Task<IActionResult> AddCity(string cityName, IFormFile fileInput)
        {
            if(string.IsNullOrEmpty(cityName))
            {
                TempData["Error"] = "Vui lòng nhập tên tỉnh / thành";
                return View();
            }

            var city = new City
            {
                Name = cityName,
            };

            if (fileInput == null)
            {
                city.PhotoPath = "/no-image.jpg";
            } 
            else
            {
                var rImage =await imageService.AddCityImage(fileInput, "cities");
                if(rImage == false)
                {
                    city.PhotoPath = "/no-image.jpg";
                } 
                else
                {
                    city.PhotoPath = $"/cities/{fileInput.FileName}";
                }
            }

            var r = await cityRepository.AddCity(city);
            if (r == false)
            {
                TempData["Error"] = "Có lỗi xảy ra. Vui lòng thử lại";
                return View();
            }

            return RedirectToAction("Index");
        }

        [Route("detail/{cityId}")]
        public async Task<IActionResult> CityDetail(int cityId)
        {
            var city = await cityRepository.GetCityById(cityId);
            ViewBag.BaseImgUrl = appConfigs.BaseImgUrl;
            return View(city);
        }

        [Route("update/{cityId}")]
        public async Task<IActionResult> Update(int cityId, City model, IFormFile fileInput)
        {
            var city = await cityRepository.GetCityById(cityId);

            if (city == null)
            {
                return RedirectToAction("Index");
            }
            city.Name = model.Name;

            if(fileInput != null)
            {
                var rImage = await imageService.UpdateCityImage(city.PhotoPath, fileInput, "cities");
                if(rImage == false)
                {
                    city.PhotoPath = "/no-image.jpg";
                }
                else
                {
                    city.PhotoPath = $"/cities/{fileInput.FileName}";
                }
            }

            var r = await cityRepository.UpdateCity(city);
            return RedirectToAction("CityDetail", new { cityId = cityId });
        }

        [Route("delete-city")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            var city = await cityRepository.GetCityById(cityId);

            if(city == null)
            {
                return RedirectToAction("Index");
            }
            var r = await cityRepository.Delete(city);
            return RedirectToAction("Index");
        }


    }
}
