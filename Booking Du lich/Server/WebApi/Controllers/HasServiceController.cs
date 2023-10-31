using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.HasService;
using WebApi.DTOs.RoomPrice;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HasServiceController : ControllerBase
    {
        private readonly IHasServiceRepository hasServiceRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IHotelRepository hotelRepository;

        public HasServiceController(IHasServiceRepository hasServiceRepository, IHotelRepository hotelRepository, IServiceRepository serviceRepository) { 
            this.hasServiceRepository = hasServiceRepository;
            this.serviceRepository = serviceRepository; 
            this.hotelRepository = hotelRepository; }

        [HttpPost("add-has-service")]
        public async Task<IActionResult> AddHasService([FromForm] AddHasServiceDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var hotel = await hotelRepository.GetHotelById(model.HotelID);
            var service = await serviceRepository.GetServiceById(model.ServiceID);

            var newHasService = new HasService
            {
                ServiceID = model.ServiceID,
                Service = service,
                HotelID = model.HotelID,
                Hotel = hotel
            };

            var result = await hasServiceRepository.AddHasService(newHasService);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add has service" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add has service successfully", HasService = newHasService }));
        }

        [HttpGet("get-all-has-service")]
        public async Task<IActionResult> GetAllHasServices()
        {
            var hasServices = await hasServiceRepository.GetAllHasService();

            return Ok(new JsonResult(new { title = "Success", HasService = hasServices }));
        }

        [HttpGet("get-has-service-by-id")]
        public async Task<IActionResult> GetHasSeriveByID(int hotelID, int serviceID)
        {
            var hasService = await hasServiceRepository.GetHasServiceByID(hotelID, serviceID);
            if (hasService == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "HasService isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", HasService = hasService }));
        }

        [HttpGet("search-service-by-hotel")]
        public async Task<IActionResult> SearchServiceOfHotel(int hotelID)
        {
            var hasServices = await hasServiceRepository.SearchServiceByHotel(hotelID);

            return Ok(new JsonResult(new { title = "Success", HasServices = hasServices }));
        }

        [HttpGet("search-hotel-by-service")]
        public async Task<IActionResult> SearchHotelOfService(int serviceId)
        {
            var hasServices = await hasServiceRepository.SearchHotelByService(serviceId);


            return Ok(new JsonResult(new { title = "Success", HasServices = hasServices }));
        }

        [HttpDelete("delete-has-service")]
        public async Task<IActionResult> DeleteHasService([FromQuery] int hotelID, [FromQuery] int serviceId)
        {
            if (hotelID == null || serviceId ==null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var hasService = await hasServiceRepository.GetHasServiceByID(hotelID, serviceId);

            if (hasService == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Has Service was not found" }));
            }

            var result = await hasServiceRepository.DeleteHasService(hasService);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Has service delete successfully" }));
        }

        
    }
}
