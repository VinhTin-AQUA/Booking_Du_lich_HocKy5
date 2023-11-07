using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.City;
using WebApi.DTOs.Service;
using WebApi.DTOs.TourType;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourTypeController : ControllerBase
    {
        private readonly ITourTypeRepository _tourTypeRepository;

        public TourTypeController(ITourTypeRepository _tourTypeRepository)
        {
            this._tourTypeRepository = _tourTypeRepository;
        }

        [HttpPost("add-tourtype")]
        public async Task<IActionResult> AddTourType([FromForm] AddTourTypeDto model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add tour type" }));
            }

            if (await _tourTypeRepository.TourTypeExisted(model.TourTypeName) == true)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Loại tour này đã tồn tại" }));
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var tourType = new TourType
            {
                TourTypeName = model.TourTypeName,
            };
            var result = await _tourTypeRepository.AddTourType(tourType);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add Tour type" }));
            }

            return Ok(new JsonResult(new { title = "Success", message = "Add tour type successfully", newTourType = tourType }));
        }

        [HttpGet("get-all-tourtypes")]
        public async Task<IActionResult> GetAllService()
        {
            var types = await _tourTypeRepository.GetAllTourType();
            return Ok(types);
        }

        [HttpDelete("delete-tourtype")]
        public async Task<IActionResult> DeleteTourType([FromQuery] int id)
        {
            var type = await _tourTypeRepository.GetTourTypeById(id);

            var result = await _tourTypeRepository.Delete(type);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when delete tour type" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Delete tour type successfully" }));
        }

        [HttpGet("get-tourtype-by-id")]
        public async Task<IActionResult> GetTourTypeById([FromQuery] int id)
        {
            var type = await _tourTypeRepository.GetTourTypeById(id);
            if (type == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", mesage = "Tour type not found" }));
            }

            return Ok(type);
        }

        [HttpPut("update-tourtype")]
        public async Task<IActionResult> UpdateTourType( EditTourTypeDto model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update tour type" }));
            }

            var tourTypeExist = await _tourTypeRepository.GetTourTypeById(model.TourTypeId);
            if (tourTypeExist == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Tour type was not existed" }));
            }

            tourTypeExist.TourTypeName = model.TourTypeName;

            var resultUpdate = await _tourTypeRepository.UpdateTourType(tourTypeExist);
            if (resultUpdate == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when update tour type" }));
            }
            return Ok();
        }
    }
}
