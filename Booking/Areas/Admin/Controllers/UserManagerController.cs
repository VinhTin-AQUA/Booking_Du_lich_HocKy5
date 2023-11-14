using Booking.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("user-management")]
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly IUserManagerRepository userManagerRepository;
        private readonly IAuthenRepository authenRepository;

        public UserManagerController(IUserManagerRepository userManagerRepository, IAuthenRepository authenRepository)
        {
            this.userManagerRepository = userManagerRepository;
            this.authenRepository = authenRepository;
        }


        public async Task<IActionResult> Index([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 5, string searchString = "")
        {
            var totalUser = userManagerRepository.TotalUsers(searchString);
            ViewBag.total = totalUser % pageSize == 0 ? totalUser / pageSize : totalUser / pageSize + 1;

            if (currentPage < 0)
            {
                currentPage = 0;
            }

            else if (currentPage > ViewBag.total)
            {
                currentPage = ViewBag.total;
            }

            var users = await userManagerRepository.GetUsers(currentPage, pageSize, searchString);
            ViewBag.Users = users.ToList();
            ViewBag.currentPage = currentPage;
            ViewBag.pageSize = pageSize;

            return View((object)searchString);
        }


        [Route("detail/{userId}")]
        [HttpGet]
        public async Task<IActionResult> Detail(string userId)
        {
            var user = await authenRepository.GetUserById(userId);
            return View(user);
        }

        [Route("delete-user")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await authenRepository.GetUserByEmail(email);

            if(user == null)
            {
                return RedirectToAction("Error","Error", (object)"Không tìm thấy người dùng");
            }

            var r = await userManagerRepository.DeleteUser(user);

            if (r.Succeeded == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra");
            }
            return RedirectToAction("Index");
        }


        [Route("lock-user")]
        [HttpGet]
        public async Task<IActionResult> LockUser(string email)
        {
            var user = await authenRepository.GetUserByEmail(email);

            if (user == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy người dùng");
            }

            IdentityResult r;

            if(user.LockoutEnd == null)
            {
                r = await userManagerRepository.LockUser(user);
            }
            else
            {
                r = await userManagerRepository.UnlockUser(user);
            }

            if (r.Succeeded == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra");
            }
            return RedirectToAction("Detail", "UserManager", new {userId = user.Id });
        }


    }
}
