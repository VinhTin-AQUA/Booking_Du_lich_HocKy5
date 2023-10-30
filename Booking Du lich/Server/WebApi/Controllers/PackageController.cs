using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.HasService;
using WebApi.DTOs.Package;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository packageRepository;
        private readonly ITourRepository tourRepository;

        public PackageController(IPackageRepository packageRepository, ITourRepository tourRepository) { this.packageRepository = packageRepository;
            this.tourRepository = tourRepository;
        }

        [HttpPost("add-package")]
        public async Task<IActionResult> AddPackage([FromForm] AddPackageDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }



            var tour = await tourRepository.GetTourById(model.TourID);
            

            var newPackage = new Package
            {
               TourID = model.TourID,
               PackageName= model.PackageName,
               Decription = model.Decription,
               MaxPeople = model.MaxPeople,
               Tour = tour,
            };

            var result = await packageRepository.AddPackage(newPackage);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add package" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add package successfully", Package = newPackage }));
        }

        [HttpGet("get-all-packages")]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await packageRepository.GetAllPackages();

            return Ok(new JsonResult(new { title = "Success", Packages = packages }));
        }
        [HttpGet("get-package-by-id")]
        public async Task<IActionResult> GetPackageById(int packageId)
        {
            var package = await packageRepository.GetPackageById(packageId);
            if (package == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Package isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", Package = package }));
        }

        [HttpGet("search-package-of-tour")]
        public async Task<IActionResult> SearchPackageOfTour(int tourID)
        {
            var packages = await packageRepository.SearchPackageOfTour(tourID);


            return Ok(new JsonResult(new { title = "Success", Packages = packages }));
        }

        [HttpGet("search-package-by-max-people")]
        public async Task<IActionResult> SearchPackageByMaxPeople(int maxPeople)
        {
            var packages = await packageRepository.SearchPackageByMaxPeople(maxPeople);


            return Ok(new JsonResult(new { title = "Success", Packages = packages }));
        }

        [HttpDelete("delete-package")]
        public async Task<IActionResult> DeletePackage([FromQuery] int packageId)
        {
            if (packageId == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var package = await packageRepository.GetPackageById(packageId);

            if (package == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Package was not found" }));
            }

            var result = await packageRepository.DeletePackage(package);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Package delete successfully" }));
        }

        [HttpPut("update-package")]
        public async Task<IActionResult> UpdatePackage([FromQuery] EditPackageDTO model)
        {
           
            var package = await packageRepository.GetPackageById(model.PackageID);

            if (package == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Package was not found" }));
            }

            package.MaxPeople = model.MaxPeople;
            package.PackageName = model.PackageName;
            package.Decription = model.Decription;

            var result = await packageRepository.UpdatePackage(package);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Package delete successfully" }));
        }
    }
}
