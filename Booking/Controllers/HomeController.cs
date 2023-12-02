using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

    }
}