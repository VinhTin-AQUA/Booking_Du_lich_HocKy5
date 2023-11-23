using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.AgentHotel.Controllers
{
	[Area("AgentHotel")]
	[Route("business-infor")]
	public class BusinessInfoController : Controller
	{
		private readonly IAuthenRepository authenRepository;
		private readonly IBusinessPartnerRepository businessPartnerRepository;

		public BusinessInfoController(IAuthenRepository authenRepository,
			IBusinessPartnerRepository businessPartnerRepository)
		{
			this.authenRepository = authenRepository;
			this.businessPartnerRepository = businessPartnerRepository;
		}

		public async Task<IActionResult> Index()
		{
			var user = await authenRepository.GetUserSignedIn(User);
			if (user == null)
			{
				return RedirectToAction("Login", "Authentication", new { area = "Authentication" });
			}
			var model = await businessPartnerRepository.GetBusinessPartnerById(user.PartnerId);
			return View(model);
		}

		[Route("update-partner")]
		[HttpPost]
		public async Task<IActionResult> UpdatePartner(BusinessPartner model)
		{
			if (model == null)
			{
				return RedirectToAction("Index");
			}
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var partner = await businessPartnerRepository.GetBusinessPartnerById(model.Id);
			if (partner == null)
			{
				return RedirectToAction("Index");
			}

			partner.PartnerName = model.PartnerName;
			partner.Email = model.Email;
			partner.Address = model.Address;
			partner.PhoneNumber = model.PhoneNumber;
			var r = await businessPartnerRepository.UpdateBusinessPartner(partner);

			return RedirectToAction("Index");
		}
	}
}
