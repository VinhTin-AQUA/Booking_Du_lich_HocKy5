using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Authentication;
using WebApi.DTOs.BusinessPartner;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IBusinessPartnerRepository partnerRepository;
        private readonly IAuthenRepository authenRepository;
        private readonly IEmailSender emailSender;

        public BusinessPartnerController(ApplicationDbContext context, 
            IBusinessPartnerRepository partnerRepository,
            IAuthenRepository authenRepository,
            IEmailSender emailSender)
        {
            this.context = context;
            this.partnerRepository = partnerRepository;
            this.authenRepository = authenRepository;
            this.emailSender = emailSender;
        }

        [HttpPost("add-businesspartner")]
        public async Task<IActionResult> AddBusinessPartner(AddBusinessPartner addBusinessPartner)
        {
            BusinessPartner businessPartner = new BusinessPartner()
            {
                PartnerName = addBusinessPartner.PartnerName,
                Address = addBusinessPartner.Address,
                Email = addBusinessPartner.Email,
                PhoneNumber = addBusinessPartner.PhoneNumber,
            };
           
            var r = await partnerRepository.AddBusinessPartner(businessPartner);
            if (r == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete-businesspartner")]
        public async Task<IActionResult> DeleteBusinessPartner([FromQuery] int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var bp = await partnerRepository.GetBusinessPartnerById(id);
            if(bp == null)
            {
                return BadRequest();
            }
            var result = await partnerRepository.DeleteBusinessPartner(bp);

            if(result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("update-businesspartner")]
        public async Task<IActionResult> UpdateBusinessPartner(AddBusinessPartner model)
        {
            // tim bp theo id
            var bp = await partnerRepository.GetBusinessPartnerById(model.Id);

            if(bp == null)
            {
                return BadRequest();
            }
            bp.PartnerName = model.PartnerName;
            bp.Address = model.Address;
            bp.Email = model.Email;
            bp.PhoneNumber = model.PhoneNumber;

            var b = await partnerRepository.UpdateBusinessPartner(bp);
            if(b == true)
            {
                return Ok();
            }
            return BadRequest();          
        }

        [HttpGet("get-businesspartness-by-id")]
        public async Task<IActionResult> GetBusinessPartnerById([FromQuery] int id)
        {
            // linq lay
            var bp = await partnerRepository.GetBusinessPartnerById(id);
            if(bp == null)
            {
                return BadRequest();
            }
            return Ok();
           

        }

        [HttpGet("get-all-businesspartner")]
        public async Task<IActionResult> GetAllBusinessPartner()
        {
            var bp = await partnerRepository.GetAllGetAllBusinessPartner();
            return Ok(bp);
        }

        [HttpPost("add-agent-hotel")]
        public async Task<IActionResult> AddAgentHotel(AddAgent model)
        {
            if(model == null)
            {
                return BadRequest(new { message = "" });
            }
            var busPart = await partnerRepository.GetBusinessPartnerById(model.BusPartId);
            var userExists = await authenRepository.GetUserByEmail(model.Email);

            if (busPart == null)
            {
                return BadRequest(new { message = "" });
            }

            if (busPart == null || userExists != null)
            {
                return BadRequest(new { message = "Email này đã được đăng ký. Vui lòng sử dụng Email khác." });
            }

            var agent = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                BusinessPartner = busPart,
                PartnerId = model.BusPartId,
                UserName = model.Email
            };

            var result = await authenRepository.CreateUser(agent, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Register failed. Please try agian." }));
            }
            await authenRepository.AddRoleToUser(agent, "AgentHotel");

            if (await emailSender.SendEmailConfirmAsync(agent, "Rất vui khi bạn sử dụng ứng dụng của chúng tôi. Hãy xác thực tài khoản đối tác của bạn."))
            {
                return Ok(new JsonResult(new
                {
                    Status = "Success",
                    Message = $"User created successfully and Send email to {agent.Email}"
                }));
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response
            {
                Status = "Error",
                Message = $"Something error. Please try again"
            });
        }

        [HttpGet("get-business-partner-by-user")]
        public async Task<IActionResult> GetBusinessPartnerByUser([FromQuery] string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
             
            var user = await authenRepository.GetUserById(userId);
            if(user == null)
            {
                return BadRequest();
            }

            var bp = await partnerRepository.GetBusinessPartnerByUser(user);
            if (bp == null)
            {
                return BadRequest(new {message = "Không tìm thấy thông tin khách sạn đối tác"});
            }
            return Ok(bp);
        }

    }
}
