using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Hotel;
using WebApi.DTOs.Service;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository _serviceRepository)
        {
            this._serviceRepository = _serviceRepository;
        }

        [HttpPost("add-service")]
        public async Task<IActionResult> AddService(AddServiceDto model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add service" }));
            }

            if (await _serviceRepository.ServiceExisted(model.ServiceName) == true)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Service name has been already existed" }));
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var hotelService = new HotelService
            {
                ServiceName = model.ServiceName,
            };
            var result = await _serviceRepository.AddService(hotelService);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add service" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add service successfully", newHotelService = hotelService }));
        }

        [HttpGet("get-all-services")]
        public async Task<IActionResult> GetAllService()
        {
            var services = await _serviceRepository.GetAllService();
            return Ok(services);
        }

        [HttpDelete("delete-service")]
        public async Task<IActionResult> DeleteService([FromQuery] int id)
        {
            var service = await _serviceRepository.GetServiceById(id);

            var result = await _serviceRepository.Delete(service);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when delete service" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Delete service successfully" }));
        }

        [HttpGet("get-service-by-id")]
        public async Task<IActionResult> GetServiceById([FromQuery] int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var service = await _serviceRepository.GetServiceById(id);
            if (service == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", mesage = "Service not found" }));
            }

            return Ok(service);
        }

    }
}
