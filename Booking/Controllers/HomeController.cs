using Booking.Interfaces;
using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookTourRepository _bookTourRepository;
        private readonly IPackagePriceRepository _packagePriceRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IBookTourRepository bookTourRepository, IPackagePriceRepository packagePriceRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _bookTourRepository = bookTourRepository;
            _packagePriceRepository = packagePriceRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(SeedRole.ADMIN_ROLE))
            {
                return RedirectToAction("Index", "UserManager", new {area = "Admin"});
            }

            if (User.IsInRole(SeedRole.AGENTHOTEL_ROLE))
            {
                return RedirectToAction("Index", "BusinessInfo", new { area = "AgentHotel" });
            }

            if (User.IsInRole(SeedRole.AGENTTOUR_ROLE))
            {
                return RedirectToAction("Index", "agent-tour-managent");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("search-tours")]
        [HttpGet]
        public IActionResult SearchTour()
        {
            return View();
        }

		[Route("tour-detail")]
		[HttpGet]
		public IActionResult TourDetail()
        {
            return View();
        }

        [Route("book-tour/{packageId}")]
        [HttpGet]

        public async Task<IActionResult> BookTour(int packageId)
        {
            var packagePrice =await _packagePriceRepository.GetPackagePriceByID(packageId, DateTime.Now);
            if(packagePrice == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.packageId = packageId;
            ViewBag.userId = userId;
            ViewBag.price = packagePrice.Price;
            return View();
        }

        [Route("book-tour/{packageId}")]
        [HttpPost]

        public async Task<IActionResult> BookTour(int packageId, BookTour model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(packageId == null || userId == null)
            {
                return NotFound();

            }
            BookTour newBookTour = new BookTour() {
                UserID = userId,
                PackageId = packageId,
                DepartureDate = model.DepartureDate,
                BookingDate = model.BookingDate,
                Price = model.Price
                };
            var r = await _bookTourRepository.AddBookTour(newBookTour);
            if(r == false)
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