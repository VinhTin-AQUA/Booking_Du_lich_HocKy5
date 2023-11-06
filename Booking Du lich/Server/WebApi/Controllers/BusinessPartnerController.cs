using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Authentication;
using WebApi.DTOs.BusinessPartner;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using WebApi1.Data;
using WebApi1.Models;
using WebApi1.Repositories;

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
        public async Task<IActionResult> UpdateBusinessPartner(AddBusinessPartner addBusinessPartner, [FromQuery] int id)
        {
            // tim bp theo id
            if(id == null)
            {
                return BadRequest();
            }
            var bp = await partnerRepository.GetBusinessPartnerById(id);
            if(bp == null)
            {
                return BadRequest();
            }
            bp.PartnerName = addBusinessPartner.PartnerName;
            bp.Address = addBusinessPartner.Address;
            bp.Email = addBusinessPartner.Email;
            bp.PhoneNumber = addBusinessPartner.PhoneNumber;

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
        public async Task<IActionResult> AddAgentHotel(AddAgentHotel model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            var busPart = await partnerRepository.GetBusinessPartnerById(model.BusPartId);
            var userExists = await authenRepository.GetUserByEmail(model.Email);

            if (busPart == null || userExists != null)
            {
                return BadRequest();
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
    }
}
