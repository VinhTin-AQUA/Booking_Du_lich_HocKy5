using Booking.Configs;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Seeds;
using Booking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Collections;
using System.Diagnostics;
using System.Security.Claims;

namespace Booking.Controllers
{
    //[Route("home")]
    public class HomeController : Controller
    {
		private readonly AppConfigs _appConfigs;
		private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookTourRepository _bookTourRepository;
        private readonly IPackagePriceRepository _packagePriceRepository;
        private readonly ITourRepository _tourRepository;
		private readonly ICityRepository cityRepository;
		private readonly IImageService _imageService;
        private readonly IPackageRepository _packageRepository;

        public HomeController(IPackageRepository packageRepository,
            IImageService imageService, 
            AppConfigs appConfigs, 
            ILogger<HomeController> logger, 
            UserManager<AppUser> userManager, 
            IBookTourRepository bookTourRepository, 
            IPackagePriceRepository packagePriceRepository, 
            ITourRepository tourRepository,
            ICityRepository cityRepository)
        {
            _appConfigs = appConfigs;
			_logger = logger;
            _userManager = userManager;
            _bookTourRepository = bookTourRepository;
            _packagePriceRepository = packagePriceRepository;
            _tourRepository = tourRepository;
			this.cityRepository = cityRepository;
			_imageService = imageService;
            _packageRepository = packageRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(SeedRole.ADMIN_ROLE))
            {
                return RedirectToAction("Index", "UserManager", new { area = "Admin" });
            }

            if (User.IsInRole(SeedRole.AGENTTOUR_ROLE))
            {
                return RedirectToAction("Index", "agent-tour");
            }

			if (User.IsInRole(SeedRole.EMPLOYEE_ROLE))
			{
				return RedirectToAction("Index", "Censor", new { area = "Employee" });
			}

			return View();
        }

        //[Route("home/get-tour/{cityName}")]
        //[HttpGet]
        //public async Task<IActionResult> GetTour(string? cityName)
        //{
        //    if (cityName == null)
        //    {
        //        return View("~/Views/Error/Error.cshtml");
        //    }
        //    var tourList = await _tourRepository.GetTourByCityName(cityName);
        //    List<double> prices = new List<double>();
        //    foreach (var tour in tourList)
        //    {
        //        double price = _tourRepository.GetPriceOfTour(tour);
        //        prices.Add(price);
        //    }
        //    if (tourList == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.tourList = tourList;
        //    ViewBag.BaseImgUrl = _appConfigs.BaseImgUrl;
        //    ViewBag.prices = prices;
        //    return PartialView("~/Views/Shared/partials/_ListTourPartial.cshtml");
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


		[HttpGet]
		[Route("search-tours")]
        public async Task<IActionResult> SearchTours([FromQuery]string? cityName)
        {
            if (cityName == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }

            var city = await cityRepository.GetCityByName(cityName);
            if(city == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }

            var tourList = await _tourRepository.GetTourByCityName(cityName);
            
            List<double> prices = new List<double>();
            List<string> imgUrls = new List<string>();
              
			foreach (var tour in tourList)
            {
                var price = await _tourRepository.GetMinPriceOfTour(tour);
                prices.Add(price);

                var imgUrlsTemp = _imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());

                if(imgUrlsTemp != null)
                {
                    imgUrls.Add(imgUrlsTemp[0]);
                }
                else
                {
                    imgUrls.Add("no-image.jpg");
                }
            }

            if (tourList == null)
            {
                return NotFound();
            }

            ViewBag.imgUrls = imgUrls;
            ViewBag.tourList = tourList;
            ViewBag.BaseImgUrl = _appConfigs.BaseImgUrl;
            ViewBag.prices = prices;
            ViewBag.city = city;
            ViewBag.CategoryId = -1;
			return View();
        }


        [Route("search-tours-by-category")]
        [HttpGet]
        public async Task<IActionResult> SearchTourByCategory(int categoryId, int cityId)
        {
            var city = await cityRepository.GetCityById(cityId);
            ICollection<Tour> tourList = new List<Tour>();
            if (categoryId <= -1)
            {
                tourList = await _tourRepository.GetTourByCityName(city.Name);
            }
            else
            {
                tourList = await _tourRepository.GetTourByCategory(categoryId, cityId);
            }

            List<double> prices = new List<double>();
            List<string> imgUrls = new List<string>();
            foreach (var tour in tourList)
            {
                var price = await _tourRepository.GetMinPriceOfTour(tour);
                prices.Add(price);
                var imgUrlsTemp = _imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());

                if (imgUrlsTemp != null)
                {
                    imgUrls.Add(imgUrlsTemp[0]);
                }
                else
                {
                    imgUrls.Add("no-image.jpg");
                }
            }

            if (tourList == null)
            {
                return NotFound();
            }

            ViewBag.imgUrls = imgUrls;

            ViewBag.tourList = tourList;
            ViewBag.BaseImgUrl = _appConfigs.BaseImgUrl;
            ViewBag.prices = prices;
            ViewBag.city = city;
            ViewBag.CategoryId = categoryId;
            return View("SearchTours");
        }


        [Route("tour-detail/{tourId}")]
        [HttpGet]
        public async Task<IActionResult> TourDetail(int? tourId)
        {
            var tour = await _tourRepository.GetTourById(tourId);

            if (tour == null)
            {
                return NotFound();
            }
            var imgUrls = _imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());
            ViewBag.ImgUrls = imgUrls;

            var packages = await _packageRepository.GetPackagesOfTour(tour.TourId);
            ViewBag.Packages = packages;


            return View(tour);

            
        }

        [Route("book-tour/{packageId}")]
        [HttpGet]
        public async Task<IActionResult> BookTour(int packageId)
        {
            var packagePrice = await _packagePriceRepository.GetPackagePriceByPackageId(packageId);
            if (packagePrice == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.packageId = packageId;
            ViewBag.userId = userId;
            ViewBag.price = packagePrice.AdultPrice;
            return View();
        }

        [Route("book-tour/{packageId}")]
        [HttpPost]
        public async Task<IActionResult> BookTour(int packageId, BookTour model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (packageId == null || userId == null)
            {
                return NotFound();

            }
            BookTour newBookTour = new BookTour()
            {
                UserID = userId,
                PackageId = packageId,
                DepartureDate = model.DepartureDate,
                BookingDate = model.BookingDate,
                Price = model.Price
            };
            var r = await _bookTourRepository.AddBookTour(newBookTour);
            if (r == false)
            {
                return View();
            }

            return RedirectToAction("Success");
        }

        [Route("success")]
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}