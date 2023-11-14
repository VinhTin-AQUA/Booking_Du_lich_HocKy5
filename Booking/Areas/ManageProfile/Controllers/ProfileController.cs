using Booking.Areas.ManageProfile.Models.Profile;
using Booking.Interfaces;
using Booking.Models;
using Booking.Models.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.ManageProfile.Controllers
{
    [Area("ManageProfile")]
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly IAuthenRepository authenRepository;

        
        public ProfileController(IAuthenRepository authenRepository)
        {
            this.authenRepository = authenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await LoadUser();
            ViewBag.User = user;
            return View();
        }

        [Route("update-profile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(EditProfile model)
        {
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            if(ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            var user = await authenRepository.GetUserByEmail(model.Email);

            if(user == null)
            {
                return RedirectToAction("Logout", "Authentication");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            var result = await authenRepository.UpdateAccount(user);
            if(result.Succeeded == false)
            {
                Notice notice = new Notice()
                {
                    Title = "Có lỗi khi cập nhật",
                    Description = "Xin vui lòng thử lại"
                };
                return RedirectToAction("Notification", "Notice", notice);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("change-password/{email}")]
        public IActionResult ChangePassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        [Route("change-password/{email}")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if(ModelState.IsValid == false)
            {
                return View();
            }

            var user = await authenRepository.GetUserByEmail(model.Email);

            if(user == null)
            {
                return RedirectToAction("Logout", "Authentication");
            }

            var result = await authenRepository.ChangePassword(user, model.OldPassword, model.NewPassword);
            if(result.Succeeded==false)
            {
                Notice notice = new Notice
                {
                    Title = "Có lỗi xảy ra. Xin vui lòng thử lại",
                    Description = ""
                };
                return RedirectToAction("Notification", "Notice", notice);
            }

            return RedirectToAction("Index");
        }


        private async Task<AppUser> LoadUser()
        {
            var user = await authenRepository.GetUserSignedIn(User);
            return user;
        }

    }
}
