using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.AgentTour.Controllers
{
    [Area("AgentTour")]
    [Route("package-price")]
    public class PackagePriceController : Controller
    {
        private readonly IPackagePriceRepository packagePriceRepository;
        private readonly IPackageRepository packageRepository;

        public PackagePriceController(IPackagePriceRepository packagePriceRepository,
            IPackageRepository packageRepository)
        {
            this.packagePriceRepository = packagePriceRepository;
            this.packageRepository = packageRepository;
        }

        public async Task<IActionResult> Index(int packageId)
        {
            var packagePrice = await packagePriceRepository.GetPackagePricesOfPackge(packageId);
            ViewBag.PackagePrice = packagePrice;
            ViewBag.PackageId = packageId;
            return View();
        }

        [Route("add-price/{packageId}")]
        public IActionResult AddPrice(int packageId)
        {
            ViewBag.PackageId = packageId;
            return View();
        }

        [Route("add-price/{packageId}")]
        [HttpPost]
        public async Task<IActionResult> AddPrice(int packageId, PackagePrice model)
        {
            if(ModelState.IsValid == false)
            {
                return View();
            }
            var package = await packageRepository.GetPackageById(packageId);

            if (package == null)
            {
                return View();
            }
            var packagePrice = new PackagePrice
            {
                AdultPrice = model.AdultPrice,
                ChildPrice = model.ChildPrice,
                ValidFrom = model.ValidFrom,
                GoodThru = model.GoodThru,
                Package = package,
                PackageId = packageId
            };
            var r = await packagePriceRepository.AddPackPrice(packagePrice);
            if(r == false)
            {
                return View(packagePrice);
            }
            return RedirectToAction("Index", new { packageId });
        }


        [Route("edit-price/{packagePriceId}")]
        public async Task<IActionResult> EditPrice(int packagePriceId)
        {
            var packagePrice = await packagePriceRepository.GetPackagePriceById(packagePriceId);

            return View(packagePrice);
        }

        [Route("edit-price/{packagePriceId}")]
        [HttpPost]
        public async Task<IActionResult> EditPrice(PackagePrice model)
        {
            var packagePrice = await packagePriceRepository.GetPackagePriceById(model.PriceId);

            if(packagePrice == null)
            {
                return RedirectToAction("Error", "Error");
            }
            packagePrice.AdultPrice = model.AdultPrice;
            packagePrice.ChildPrice = model.ChildPrice;
            packagePrice.ValidFrom = model.ValidFrom;
            packagePrice.GoodThru = model.GoodThru;

            if (packagePrice.ValidFrom > packagePrice.GoodThru)
            {
                ModelState.AddModelError("ValidFrom", "Ngày áp dựng không thể lớn hơn ngày hiện tại");
                ModelState.AddModelError("GoodThru", "Ngày áp dựng không thể lớn hơn ngày hiện tại");
                return View(packagePrice);
            }

            var r = await packagePriceRepository.UpdatePackagePrice(packagePrice);

            if(r == false)
            {
                return RedirectToAction("Error", "Error");
            }

            return View(packagePrice);
        }

    }
}
