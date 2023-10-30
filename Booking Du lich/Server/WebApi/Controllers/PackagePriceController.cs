using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.PackagePrice;
using WebApi.DTOs.RoomPrice;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagePriceController : ControllerBase
    {
        private readonly IPackagePriceRepository packagePriceRepository;
        private readonly IPackageRepository packageRepository;

        public PackagePriceController(IPackagePriceRepository packagePriceRepository, IPackageRepository packageRepository) {
            this.packagePriceRepository = packagePriceRepository;
            this.packageRepository = packageRepository;
        }

        [HttpPost("add-package-price")]
        public async Task<IActionResult> AddPackagePrice([FromForm] AddPackagePriceDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }



            var package = await packageRepository.GetPackageById(model.PackageId);

            var newPackagePrice = new PackagePrice
            {
                Price = model.Price,
                PackageId = model.PackageId,
                ValidFrom = DateTime.Now,
                Package = package
            };

            var result = await packagePriceRepository.AddPackPrice(newPackagePrice);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add package price" }));
            }



            return Ok(new JsonResult(new { title = "Success", message = "Add package price successfully", PackPrice = newPackagePrice }));
        }


        [HttpGet("get-all-package-price")]
        public async Task<IActionResult> GetAllPackagePrices()
        {
            var packagePrices = await packagePriceRepository.GetAllPackagePrices();

            return Ok(new JsonResult(new { title = "Success", PackagePrices = packagePrices }));
        }

        [HttpGet("get-package-price-by-price")]
        public async Task<IActionResult> GetPackagePriceByPrice(double price)
        {
            var packagePrice = await packagePriceRepository.GetPackagePriceByPrice(price);
            if (packagePrice == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Package price isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", PackagePrice = packagePrice }));
        }

        [HttpGet("get-package-price-by-id")]
        public async Task<IActionResult> GetPackagePriceById(int packageId, DateTime validFrom)
        {
            var packagePrice = await packagePriceRepository.GetPackagePriceByID(packageId, validFrom);
            if (packagePrice == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Package price isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", PackagePrice = packagePrice }));
        }


        [HttpDelete("delete-package-price")]
        public async Task<IActionResult> DeleteRoom([FromQuery] int? packageId, [FromQuery] DateTime? validFrom)
        {
            if (packageId == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var packagePrice = await packagePriceRepository.GetPackagePriceByID(packageId, validFrom);

            if (packagePrice == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Package price was not found" }));
            }

            var result = await packagePriceRepository.DeletePackagePrice(packagePrice);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Package price delete successfully" }));
        }

        [HttpPut("update-package-price")]
        public async Task<IActionResult> UpdatePackagePrice([FromForm] EditPackagePriceDTO model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update package price" }));
            }

            var packagePriceExisted = await packagePriceRepository.GetPackagePriceByID(model.PackageID, model.ValidFrom);
            if (packagePriceExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Package Price was not existed" }));
            }
            packagePriceExisted.Price = model.Price;

            var resultUpdate = await packagePriceRepository.UpdatePackagePrice(packagePriceExisted);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update package price" }));
            }


            return Ok(new JsonResult(new { title = "Success", message = "Update package price successfully" }));
        }
    }
}
