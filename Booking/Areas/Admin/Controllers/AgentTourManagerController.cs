using Booking.Areas.Admin.Models.AgentHotelManager;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Seeds;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("agent-tour")]
    public class AgentTourManagerController : Controller
    {
        private readonly ITourRepository tourRepository;
        private readonly IAuthenRepository authenRepository;
        private readonly IUserManagerRepository userManagerRepository;

        public AgentTourManagerController(ITourRepository tourRepository, IAuthenRepository authenRepository, IUserManagerRepository userManagerRepository)
        {
            this.tourRepository = tourRepository;
            this.authenRepository = authenRepository;
            this.userManagerRepository = userManagerRepository;
        }

        public async Task<IActionResult> Index([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 5, string searchString = "")
        {
            var totalUser = await tourRepository.TotalAgent(searchString);
            ViewBag.total = totalUser % pageSize == 0 ? totalUser / pageSize : totalUser / pageSize + 1;

            if (currentPage < 0)
            {
                currentPage = 0;
            }

            else if (currentPage > ViewBag.total)
            {
                currentPage = ViewBag.total;
            }

            var users = await tourRepository.GetAgentTours(currentPage, pageSize, searchString);
            ViewBag.Users = users.ToList();
            ViewBag.currentPage = currentPage;
            ViewBag.pageSize = pageSize;

            return View((object)searchString);
        }

        [Route("add-account-agent-tour")]
        public IActionResult AddAccount() 
        {
            AddAgent addAgent = new AddAgent();
            return View(addAgent);
        }

        [Route("add-account-agent-tour")]
        [HttpPost]
        public async Task<IActionResult> AddAccount(AddAgent model)
        {
            if (model == null)
            {
                return View(model);
            }
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var newAgent = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var r = await authenRepository.SignUp(newAgent, model.Password);
            if (r.Succeeded == false)
            {
                ModelState.AddModelError("Email", "Email này đã đăng ký. Vui lòng chọn email khác.");
                return View(model);
            }
            await authenRepository.AddRoleToUser(newAgent, SeedRole.AGENTTOUR_ROLE);
            await authenRepository.UpdateAccount(newAgent);
            return RedirectToAction("Index");
        }

        [Route("agent-tour-detail/{id}")]
        public async Task<IActionResult> AgentTourDetail(string id)
        {
            var agent = await authenRepository.GetUserById(id);
            if(agent == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy thông tin");
            }
            return View(agent);
        }

        [Route("delete-agent-tour")]
        public async Task<IActionResult> DeleteAgentTour(string email)
        {
            var user = await authenRepository.GetUserByEmail(email);
            if (user == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy người dùng");
            }
            var r = await userManagerRepository.DeleteUser(user);

            if (r.Succeeded == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra");
            }
            return RedirectToAction("Index");
        }
    }
}
