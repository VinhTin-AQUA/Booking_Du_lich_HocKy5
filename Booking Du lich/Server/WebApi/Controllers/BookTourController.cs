using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BookRoom;
using WebApi.DTOs.BookTour;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTourController : ControllerBase
    {
       
        private readonly IAuthenRepository authenRepository;
        private readonly IBookTourRepository bookTourRepository;
        private readonly IPackageRepository packageRepository;

        public BookTourController(IBookTourRepository bookTourRepository, IPackageRepository packageRepository, IAuthenRepository authenRepository)
        {
            this.bookTourRepository = bookTourRepository;
            this.packageRepository = packageRepository;
            this.authenRepository = authenRepository;
        }

        [HttpPost("add-book-tour")]
        public async Task<IActionResult> AddBookTour([FromForm] AddBookTourDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var bookTour = await bookTourRepository.GetBookTourByID(model.UserID, model.PackageID, model.DepartureDate);


            if (bookTour != null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Book Tour đã tồn tại" }));
            }

            var Package = await packageRepository.GetPackageById(model.PackageID);
            var User = await authenRepository.GetUserById(model.UserID);

            var newBookTour = new BookTour
            {
               UserID = model.UserID,
               PackageId = model.PackageID,
               DepartureDate = model.DepartureDate,
               BookingDate = model.BookingDate,
               Package = Package,
               User = User,
            };

            var result = await bookTourRepository.AddBookTour(newBookTour);

            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when add book tour " }));
            }



            return Ok(new JsonResult(new { title = "Success", message = "Add book tour  successfully", BookTour = newBookTour
            }));
        }

        [HttpGet("get-all-book-tours")]
        public async Task<IActionResult> GetAllBookTours()
        {
            var bookTours = await bookTourRepository.GetAllBookTour();

            return Ok(new JsonResult(new { title = "Success", BookTours = bookTours }));
        }

        [HttpGet("get-book-tour-by-id")]
        public async Task<IActionResult> GetBookTourByID(string userID, int packageId, DateTime? DepartureDate)
        {
            var bookTour = await bookTourRepository.GetBookTourByID(userID, packageId, DepartureDate);
            if (bookTour == null)
            {
                return NotFound(new JsonResult(new { title = "Error", message = "Book Tour isn't exist" }));
            }

            return Ok(new JsonResult(new { title = "Success", BookTour = bookTour }));
        }

        [HttpDelete("delete-book-tour")]
        public async Task<IActionResult> DeleteBookTour([FromQuery] string userID, [FromQuery] int packageId, [FromQuery] DateTime? DepartureDate)
        {
            if (userID == null || packageId == null || DepartureDate == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Missing parameter" }));
            }
            var bookTour = await bookTourRepository.GetBookTourByID(userID, packageId, DepartureDate);

            if (bookTour == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Book Tour was not found" }));
            }

            var result = await bookTourRepository.DeleteBookTour(bookTour);
            if (result == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Book Tour delete successfully" }));
        }
    }

   
}
