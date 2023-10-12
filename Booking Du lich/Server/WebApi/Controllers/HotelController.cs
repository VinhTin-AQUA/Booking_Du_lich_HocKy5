using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Hotel;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        [HttpPost("add-hotel")]
        public async Task<IActionResult> AddHotel(AddHotel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var hotel = new Hotel
            {
                HotelName = model.HotelName,
            };
            var result = await hotelRepository.AddHotel(hotel);
            if(result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add hotel" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add hotel successfully" }));
        }


     
    }
}
