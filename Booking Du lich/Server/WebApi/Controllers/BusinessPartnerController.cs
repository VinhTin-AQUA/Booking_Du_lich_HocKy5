using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BusinessPartner;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi1.Data;
using WebApi1.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IBusinessPartnerRepository partnerRepository;

        public BusinessPartnerController(ApplicationDbContext context, IBusinessPartnerRepository partnerRepository)
        {
            this.context = context;
            this.partnerRepository = partnerRepository;
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
    }
}
