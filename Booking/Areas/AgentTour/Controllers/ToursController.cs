using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Booking.Areas.AgentTour.Controllers
{
    [Area("AgentTour")]
    [Route("agent-tour")]
    public class ToursController : Controller
    {

        private readonly ITourRepository _tourRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageService _imageService;
        private readonly ITourCategoryRepository tourCategoryRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IPackageRepository packageRepository;

        public ToursController(ITourRepository tourRepository,
            UserManager<AppUser> userManager,
            IImageService imageService,
            ITourCategoryRepository tourCategoryRepository,
            ICategoryRepository categoryRepository,
            IPackageRepository packageRepository)
        {
            _tourRepository = tourRepository;
            _userManager = userManager;
            _imageService = imageService;
            this.tourCategoryRepository = tourCategoryRepository;
            this.categoryRepository = categoryRepository;
            this.packageRepository = packageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourRepository.GetAllTours(-1,-1);
            ViewBag.Tours = tours.ToList();
            return View();
        }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var tour = await _tourRepository.GetTourById(id);

            if (tour == null)
            {
                return NotFound();
            }
            var imgUrls = _imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());
            ViewBag.ImgUrls = imgUrls;

            var packages = await packageRepository.GetPackagesOfTour(tour.TourId);
            ViewBag.Packages = packages;


            return View(tour);
        }

        [Route("create")]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Tour model, List<int> tourCategoryIds, List<IFormFile> fileInputs)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Tour newTour = new Tour()
                {
                    TourName = model.TourName,
                    Overview = model.Overview,
                    Schedule = model.Schedule,
                    DepartureLocation = model.DepartureLocation,
                    DropOffLocation = model.DropOffLocation,
                    ApproverID = null,
                    PosterID = model.PosterID,
                    ApprovalDate = null,
                    PostingDate = DateTime.Now,
                    PhotoPath = "/no-image.jpg"
                };

                var r = await _tourRepository.AddTour(newTour);
                if (r == false)
                {
                    return RedirectToAction("Error");
                }

                foreach (var tourCategoryd in tourCategoryIds)
                {
                    var category = await categoryRepository.GetCategoryById(tourCategoryd);
                    var tourCategory = new TourCategory
                    {
                        CategoryId = tourCategoryd,
                        TourId = newTour.TourId,
                        Tour = newTour,
                        Category = category
                    };
                    await tourCategoryRepository.AddTourCategory(tourCategory);
                }

                if (fileInputs != null)
                {
                    var rImage = await _imageService.UploadImages(fileInputs, "tours", newTour.TourId.ToString());

                    newTour.PhotoPath = rImage;
                    await _tourRepository.UpdateTour(newTour);
                }


                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }
            var imgUrls = _imageService.GetAllFileOfFolder("tours", tour.TourId.ToString());
            ViewBag.ImgUrls = imgUrls;

            var tourCategories = await tourCategoryRepository.GetTourCategoryByTourId(tour.TourId);
            var tourCategoryIds = tourCategories.Select(tc => tc.CategoryId).ToList();
            ViewBag.TourCategories = tourCategoryIds;

            var categories = await categoryRepository.GetAllCategories();
            ViewBag.Categories = categories.ToList();

            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, Tour model, List<int> tourCategoryIds, List<IFormFile> fileInputs)
        {
            if (model == null)
            {
                return NotFound();
            }
            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                tour.TourName = model.TourName;
                tour.Overview = model.Overview;
                tour.Schedule = model.Schedule;
                tour.DepartureLocation = model.DepartureLocation;
                tour.DropOffLocation = model.DropOffLocation;

                var r = await _tourRepository.UpdateTour(tour);
                if (r == false)
                {
                    return RedirectToAction("Error");
                }


                var rRemoveOldTourCategories = await tourCategoryRepository.DeleteTourCategoriesByTourId(tour.TourId);


                if(rRemoveOldTourCategories == true)
                {
                    foreach (var tourCategoryId in tourCategoryIds)
                    {
                        var category = await categoryRepository.GetCategoryById(tourCategoryId);
                        var tourCategory = new TourCategory
                        {
                            CategoryId = tourCategoryId,
                            TourId = tour.TourId,
                            Tour = tour,
                            Category = category
                        };
                        await tourCategoryRepository.AddTourCategory(tourCategory);
                    }
                }


                if (fileInputs != null)
                {
                    var rDeleteAllImages = _imageService.DeleteAllImages("tours", tour.TourId.ToString());
                    var rImage = await _imageService.UploadImages(fileInputs, "tours", tour.TourId.ToString());
                }

                return RedirectToAction("Edit", new { id });
            }
            return RedirectToAction("Error");
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var tour = await _tourRepository.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tour = await _tourRepository.GetTourById(id);

            if (tour != null)
            {
                var r = await _tourRepository.DeleteTour(tour);
                if (r == false)
                {
                    return RedirectToAction("Error");
                }
                _imageService.DeleteAllImages("tours", id.ToString());
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
