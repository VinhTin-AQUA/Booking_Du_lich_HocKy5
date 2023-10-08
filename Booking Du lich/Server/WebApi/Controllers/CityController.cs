using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IImageService imageService;

        public CityController(ICityRepository cityRepository, 
            IWebHostEnvironment hostEnvironment,
            IImageService imageService)
        {
            this.cityRepository = cityRepository;
            this.hostEnvironment = hostEnvironment;
            this.imageService = imageService;
        }

        [HttpPost("add-city")]
        public async Task<IActionResult> AddCity(IFormFile file, [FromForm] CityDto model)
        {

            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add city" }));
            }

            if (await cityRepository.CityExisted(model.Name) == true)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "City name has been already existed" }));
            }

            //var file = Request.Form.Files[0];
            var result = await imageService.AddOneToFolder(file, "cities");

            var newCity = new City
            {
                CityCode = model.CityCode,
                Name = model.Name,
                ImgUrl = "/cities/" + file.FileName,
                Accommodations = 0
            };

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add city image" }));
            }

            if (await cityRepository.AddCity(newCity) == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add city" }));
            }

            return Ok(newCity);
        }


        [HttpGet("get-all-cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var cites = await cityRepository.GetAllCities();
            return Ok(cites.ToList());
        }
    }
}
