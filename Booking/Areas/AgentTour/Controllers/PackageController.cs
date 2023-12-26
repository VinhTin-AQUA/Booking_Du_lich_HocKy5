using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.AgentTour.Controllers
{
    [Area("AgentTour")]
    [Route("packages")]
    public class PackageController : Controller
    {
        private readonly ITourRepository tourRepository;
        private readonly IImageService imageService;
        private readonly IPackageRepository packageRepository;
        private readonly IPackagePriceRepository packagePriceRepository;

        public PackageController(ITourRepository tourRepository,
            IImageService imageService,
            IPackageRepository packageRepository,
            IPackagePriceRepository packagePriceRepository)
        {
            this.tourRepository = tourRepository;
            this.imageService = imageService;
            this.packageRepository = packageRepository;
            this.packagePriceRepository = packagePriceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await tourRepository.GetAllTours(-1,-1);
            ViewBag.Tours = tours.ToList();

            List<string> images = new List<string>();
            List<int> packagesTotal = new List<int>();

            foreach (var tour in tours)
            {
                var imgUrls = imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());

                if (imgUrls != null)
                {
                    images.Add(imgUrls[0]);
                }
                else
                {
                    images.Add(tour.PhotoPath);
                }
                var packgs = await packageRepository.GetPackagesOfTour(tour.TourId);
                packagesTotal.Add(packgs.Count());
            }
            ViewBag.Images = images;
            ViewBag.PackagesTotal = packagesTotal;
            return View();
        }

        [HttpGet]
        [Route("packages-of-tour/{id}")]
        public async Task<IActionResult> PackagesTour(int id)
        {
            var packages = await packageRepository.SearchPackageOfTour(id);
            ViewBag.Packages = packages;
            ViewBag.TourId = id;
            return View();
        }

        [HttpGet]
        [Route("add-new-package/{tourId}")]
        public IActionResult Create(int tourId)
        {
            ViewBag.TourId = tourId;
            return View();
        }

        [Route("add-new-package/{tourId}")]
        [HttpPost]
        public async Task<IActionResult> Create(int tourId, List<PackagePrice> prices, string packageName, string decription, int maxPeople)
        {
            var tour = await tourRepository.GetTourById(tourId);

            if (tour == null)
            {
                return Json(new { success = false });
            }

            var package = new Package
            {
                PackageName = packageName,
                Description = decription,
                MaxPeople = maxPeople,
                TourID = tourId,
                Tour = tour
            };

            var r = await packageRepository.AddPackage(package);

            if (r == false)
            {
                return Json(new { success = false });
            }

            foreach (var price in prices)
            {
                var packagePrice = new PackagePrice
                {
                    AdultPrice = price.AdultPrice,
                    ChildPrice = price.ChildPrice,
                    ValidFrom = price.ValidFrom,
                    GoodThru = price.GoodThru,
                    PackageId = package.PackageID,
                    Package = package
                };

                await packagePriceRepository.AddPackPrice(packagePrice);
            }

            return Json(new { success = true, tourId = tourId });
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var package = await packageRepository.GetPackageById(id);

            return View(package);
        }

        [Route("delete-package/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var package = await packageRepository.GetPackageById(id);
            return View(package);
        }

        [Route("delete-package/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var package = await packageRepository.GetPackageById(id);

            if (package == null)
            {
                return RedirectToAction("Index");
            }
            int tourId = package.TourID;

            var r = await packageRepository.DeletePackage(package);

            if (r == false)
            {
                return View(package);
            }

            return RedirectToAction("PackagesTour", new { id = tourId });
        }

        [Route("edit-package/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var package = await packageRepository.GetPackageById(id);
            return View(package);
        }

        [Route("edit-package/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string packageName, string decription, int maxPeople)
        {
            var package = await packageRepository.GetPackageById(id);

            if (package == null)
            {
                return Json(new {success = false});
            }

            package.PackageName = packageName;
            package.Description = decription;
            package.MaxPeople = maxPeople;
            var r = await packageRepository.UpdatePackage(package);
            return Json(new { success = r, packageId = package.PackageID });
        }


    }
}
