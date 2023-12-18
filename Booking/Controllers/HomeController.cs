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
        private readonly IUserManagerRepository _userManagerRepository;

        public HomeController(IPackageRepository packageRepository,
            IImageService imageService, 
            AppConfigs appConfigs, 
            ILogger<HomeController> logger, 
            UserManager<AppUser> userManager, 
            IBookTourRepository bookTourRepository, 
            IPackagePriceRepository packagePriceRepository, 
            ITourRepository tourRepository,
            ICityRepository cityRepository, IUserManagerRepository userManagerRepository)
             
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
            _userManagerRepository = userManagerRepository;
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


        [Route("get-some-tours")]
        [HttpGet]
        public async Task<IActionResult> GetTourOutStanding(string? cityName)
        {
            if (cityName == null)
            {
                return Json(new {status = false});
            }

            var city = await cityRepository.GetCityByName(cityName);
            if(city == null)
            {
                return Json(new { status = false });
            }

            var allTourtoursOfCity = await _tourRepository.GetTourByCityName(cityName);
            var tourList = allTourtoursOfCity.Take(8).ToList();

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

            List<object> tourOutstanding = new List<object>();

            for (int i = 0; i < tourList.Count(); i ++)
            {
                var tour = new
                {
                    tourId = tourList[i].TourId,
                    tourName = tourList[i].TourName,
                    overview = tourList[i].Overview,
                    price = prices[i],
                    imgUrl = _appConfigs.BaseImgUrl +"/"+ imgUrls[i]
                };
                tourOutstanding.Add(tour);
            }

            //var tourOutstanding = tourList.Select(t => new { t.TourName, t.Overview }).ToList();

            return Json(new { status = true, tourOutstanding });
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
            var packageSelected = await _packageRepository.GetPackageById(packageId);
            if (packagePrice == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return RedirectToAction("Login", "Authentication",new {returnUrl = packageSelected.TourID } );
            }
            var user = await _userManagerRepository.GetUserById(userId);

            ViewBag.packageId = packageId;
            ViewBag.userId = userId;
            ViewBag.AdultPrice = packagePrice.AdultPrice;
            ViewBag.ChildPrice = packagePrice.ChildPrice;
            ViewBag.package = packageSelected;
            ViewBag.user = user;
            return View();
        }

        [Route("book-tour/{packageId}")]
        [HttpPost]
        public async Task<IActionResult> BookTour(int packageId, BookTour model)
        {
            List<BookTourDetail> lst_bookTourDetail = new List<BookTourDetail>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (packageId == null || userId == null)
            {
                return NotFound();

            }
            model.UserID = userId;
            model.PackageId = packageId;
            model.BookingDate = DateTime.Now;

            Package package = await _packageRepository.GetPackageById(packageId);
            Tour tour = await _tourRepository.GetTourById(package.TourID);

            // Start Processing View Checkout

            ViewBag.tourName = tour.TourName;
            ViewBag.packageName = package.PackageName;
            ViewBag.price = model.Price;
            ViewBag.numOfTourist = Request.Form["totalParticipation"];
            ViewBag.departureDate = Convert.ToString(DateTime.Now.Date).Substring(0,10);

            // End Processing View Checkout

            // Start Processing BookTourDetail

            int numOfChild = Convert.ToInt32(Request.Form["quantityChild"]);
            int numOfAdult = Convert.ToInt32(Request.Form["quantityAdult"]);
            for (int i = 1; i <= numOfAdult; ++i)
            {
                BookTourDetail btd = new BookTourDetail()
                {
                    FirstNameTourist = Convert.ToString(Request.Form[$"firstNameAdult-{i}"]),
                    LastNameTourist = Convert.ToString(Request.Form[$"lastNameAdult-{i}"]),
                    IsAdult = true
                };
                lst_bookTourDetail.Add(btd);
            }
            if (numOfChild > 0)
            {
                for (int i = 1; i <= numOfChild; ++i)
                {
                    BookTourDetail btd = new BookTourDetail()
                    {
                        FirstNameTourist = Convert.ToString(Request.Form[$"firstNameChild-{i}"]),
                        LastNameTourist = Convert.ToString(Request.Form[$"lastNameChild-{i}"]),
                        IsAdult = false
                    };
                    lst_bookTourDetail.Add(btd);
                }
            }

            ViewBag.myLst = lst_bookTourDetail;
            int a = 1;
 ViewBag.image = _imageService.GetAllFileOfFolder("tours", package.TourID.ToString())[0];
            ViewBag.BaseImgUrl = _appConfigs.BaseImgUrl;
            // End Processing BookTourDetail

            return View("Checkout");
        }

        [Route("success")]
        [HttpGet]
        public async Task<IActionResult> Success()
        {
            return View();
        }

        [Route("Checkout")]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {

            return View();
        }

    }
}