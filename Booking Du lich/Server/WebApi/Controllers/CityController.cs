using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.City;
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
        public async Task<IActionResult> AddCity(IFormFile file, [FromForm] AddCityDto model)
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

        [HttpDelete("delete-city")]
        public async Task<IActionResult> DeleteCity([FromQuery] int id)
        {
            var city = await cityRepository.GetCityById(id);

            var result = await cityRepository.Delete(city);
            imageService.DeleteCityImage(city.ImgUrl);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when delete city" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Delete city successfully" }));
        }

        [HttpPut("update-city")]
        public async Task<IActionResult> UpdateCity(IFormFile file, [FromForm] EditCityDto model)
        {
            if(model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update city" }));
            }

            var cityExist = await cityRepository.GetCityById(model.Id);
            if(cityExist == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "City was not existed" }));
            }

            // thay anh 
            if (file != null)
            {
                var result = await imageService.UpdateImage(cityExist.ImgUrl, file, "cities");
                cityExist.ImgUrl = "/cities/" + file.FileName;
            }

            cityExist.Name = model.Name;
            cityExist.CityCode = model.CityCode;
           

            var resultUpdate = await cityRepository.UpdateCity(cityExist);
            if(resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update city" }));
            }

            


            return Ok();
        }

        [HttpGet("search-cities")]
        public async Task<IActionResult> SearchCities([FromQuery] string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var cites = await cityRepository.GetAllCities();
                return Ok(cites.ToList());
            }

            var cities = await cityRepository.SearchCities(searchString);
            return Ok(cities);
        }
    }
}
