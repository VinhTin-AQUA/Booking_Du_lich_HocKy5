using Booking.Models.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    public class NoticeController : Controller
    {

        [Route("notification")]
        public IActionResult Notification(Notice notice)
        {
            ViewBag.Notice = notice;
            return View();
        }
    }
}
