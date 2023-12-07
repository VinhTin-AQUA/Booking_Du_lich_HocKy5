using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;

namespace Booking.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("employee")]
    public class CensorController : Controller
    {
        private readonly ITourRepository tourRepository;
        private readonly IImageService imageService;
        private readonly IPackageRepository packageRepository;
        private readonly UserManager<AppUser> userManager;

        public CensorController(ITourRepository tourRepository,
            IImageService imageService,
            IPackageRepository packageRepository,
            UserManager<AppUser> userManager)
        {
            this.tourRepository = tourRepository;
            this.imageService = imageService;
            this.packageRepository = packageRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await tourRepository.GetAllTours();
            ViewBag.Tours = tours.ToList();
            return View();
        }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var tour = await tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }
            var imgUrls = imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());
            ViewBag.ImgUrls = imgUrls;

            var packages = await packageRepository.GetPackagesOfTour(tour.TourId);
            ViewBag.Packages = packages;
            ViewBag.TourId = tour.TourId;

            ViewBag.HasCensored = tour.Approver == null ? false : true;
            ViewBag.Poster = tour.Poster;

            return View(tour);
        }

        [Route("duyet/{tourId}")]
        public async Task<IActionResult> Censoring(int tourId)
        {
            var tour = await tourRepository.GetTourById(tourId);

            if (tour == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var user = await userManager.GetUserAsync(User);

            tour.ApprovalDate = DateTime.Now;
            tour.Approver = user;
            tour.ApproverID = user.Id;
            var r = await tourRepository.UpdateTour(tour);
            if(r == false)
            {
                return RedirectToAction("Error", "Error");
            }
            return RedirectToAction("Index");
        }

        [Route("huy-duyet/{tourId}")]
        public async Task<IActionResult> UnCensoring(int tourId)
        {
            var tour = await tourRepository.GetTourById(tourId);

            if (tour == null)
            {
                return RedirectToAction("Error", "Error");
            }


            tour.ApprovalDate = null;
            tour.Approver = null;
            tour.ApproverID = null;
            var r = await tourRepository.UpdateTour(tour);
            if (r == false)
            {
                return RedirectToAction("Error", "Error");
            }
            return RedirectToAction("Index");
        }
    }
}
