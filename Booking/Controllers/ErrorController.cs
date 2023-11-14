using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("page-not-found")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("error")]
        public IActionResult Error(string error)
        {
            return View(error);
        }
    }
}
