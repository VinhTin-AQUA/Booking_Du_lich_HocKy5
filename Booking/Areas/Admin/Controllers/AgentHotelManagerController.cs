using Booking.Areas.Admin.Models.AgentHotelManager;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Seeds;
using Booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("agent-hotel-manager")]
    public class AgentHotelManagerController : Controller
    {
        private readonly IBusinessPartnerRepository businessPartnerRepository;
        private readonly IAuthenRepository authenRepository;
        private readonly IUserManagerRepository userManagerRepository;
        private readonly IEmailSender emailSender;

        public AgentHotelManagerController(IBusinessPartnerRepository businessPartner,
            IAuthenRepository authenRepository,
            IUserManagerRepository userManagerRepository,
            IEmailSender emailSender)
        {
            this.businessPartnerRepository = businessPartner;
            this.authenRepository = authenRepository;
            this.userManagerRepository = userManagerRepository;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 5, string searchString = "")
        {
            var totalPartners = businessPartnerRepository.TotalPartners(searchString);
            ViewBag.total = totalPartners % pageSize == 0 ? totalPartners / pageSize : totalPartners / pageSize + 1;

            if (currentPage < 0)
            {
                currentPage = 0;
            }

            else if (currentPage > ViewBag.total)
            {
                currentPage = ViewBag.total;
            }

            var partners = await businessPartnerRepository.GetAllBusinessPartner(currentPage, pageSize, searchString);
            ViewBag.Partners = partners.ToList();
            ViewBag.currentPage = currentPage;
            ViewBag.pageSize = pageSize;

            return View((object)searchString);
        }

        [Route("add-business")]
        public IActionResult AddBusiness()
        {
            return View();
        }

        [Route("add-business")]
        [HttpPost]
        public async Task<IActionResult> AddBusiness(AddBusiness model)
        {
            if(model == null)
            {
                return View();
            }

            if(ModelState.IsValid == false) 
            {
                return View(model);
            }

            BusinessPartner newBus = new BusinessPartner()
            {
                PartnerName = model.PartnerName,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var r = await businessPartnerRepository.AddBusinessPartner(newBus);

            if (r == false)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [Route("detail-partner/{partnerId}")]
        public async Task<IActionResult> Detail(int partnerId)
        {
            var partner = await businessPartnerRepository.GetBusinessPartnerById(partnerId);
            if (partner == null)
            {
                return RedirectToAction("Index");
            }

            var agentHotels = await businessPartnerRepository.GetAgentsPartner(partnerId);
            ViewBag.AgentHotels = agentHotels;
            ViewBag.PartnerId = partner.Id;

            return View(partner);
        }

        [Route("delete-partner/{partnerId}")]
        [HttpGet]
        public async Task<IActionResult> DeletePartner(int partnerId)
        {
            var partner = await businessPartnerRepository.GetBusinessPartnerById(partnerId);
            if (partner == null)
            {
                return RedirectToAction("Index");
            }

            var rDeleteAgents = await businessPartnerRepository.DeleteAgents(partner);

            if (rDeleteAgents == false)
            {
                return RedirectToAction("Detail", new { partnerId = partnerId });
            }

            var r = await businessPartnerRepository.DeleteBusinessPartner(partner);

            if (r == false)
            {
                return View(partner);
            }
            return RedirectToAction("Index");
        }

        [Route("add-account-agent-hotel")]
        public IActionResult AddAccount(int partnerId)
        {
            ViewBag.PartnerId = partnerId;
            AddAgent model = new AddAgent();
			return View(model);
        }

        [Route("add-account-agent-hotel")]
        [HttpPost]
        public async Task<IActionResult> AddAccount(AddAgent model, int partnerId)
        {
            if(model == null)
            {
				ViewBag.PartnerId = partnerId;
				return View(model);
            }

            if (ModelState.IsValid == false)
            {
				ViewBag.PartnerId = partnerId;
				return View(model);
			}

            var partner = await businessPartnerRepository.GetBusinessPartnerById(partnerId);
            if (partner == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy thông tin");
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

            newAgent.PartnerId = partnerId;
            newAgent.BusinessPartner = partner;


            var r = await authenRepository.SignUp(newAgent, model.Password);
            if(r.Succeeded == false)
            {
                ModelState.AddModelError("Email", "Email này đã đăng ký.Vui lòng chọn email khác.");
                ViewBag.PartnerId = partnerId;
                return View(model);
            }
            await authenRepository.AddRoleToUser(newAgent, SeedRole.AGENTHOTEL_ROLE);
            await authenRepository.UpdateAccount(newAgent);


            var token = await authenRepository.GenerateEmailConfirmationToken(newAgent);

            if (await emailSender.SendEmailConfirmAsync(newAgent, "confirm-email", token) == true)
            {
                return RedirectToAction("Detail", new { partnerId = partnerId });
            }

            return RedirectToAction("Detail", new { partnerId = partnerId });
        }

        [Route("agent-detail/{agentId}")]
        public async Task<IActionResult> AgentDetail(string agentId)
        {
            var agent = await authenRepository.GetUserById(agentId);
            return View(agent);
        }

        [Route("delete-agent")]
        public async Task<IActionResult> DeleteAgentHotel(string email)
        {
            var user = await authenRepository.GetUserByEmail(email);

            if (user == null)
            {
                return RedirectToAction("Error", "Error", (object)"Không tìm thấy người dùng");
            }
            int? partnerId = user.PartnerId;
            var r = await userManagerRepository.DeleteUser(user);

            if (r.Succeeded == false)
            {
                return RedirectToAction("Error", "Error", (object)"Có lỗi xảy ra");
            }
            return RedirectToAction("Detail", new { partnerId = partnerId });
        }

    }
}
