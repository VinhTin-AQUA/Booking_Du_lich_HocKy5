using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class BusinessPartnerRespository : IBusinessPartnerRepository
    {
        private readonly ApplicationDbContext context ;
        public BusinessPartnerRespository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Save()
        {
            var r = await this.context.SaveChangesAsync();
            return r > 0;
        }
        public async Task<bool> AddBusinessPartner(BusinessPartner businessPartner)
        {
            context.BusinessPartner.Add(businessPartner);
            return await Save();
        }
        public async Task<bool> DeleteBusinessPartner(BusinessPartner businessPartner)
        {
            if(businessPartner == null) 
            {
                return false;
            }
            context.BusinessPartner.Remove(businessPartner);
            return await Save();
        }
        public async Task<BusinessPartner> GetBusinessPartnerById(int? id)
        {
            var bp = await context.BusinessPartner
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return bp;
        }

        public async Task<BusinessPartner> GetBusinessPartnerByUser(ApplicationUser user)
        {
            if (user.BusinessPartner == null)
            {
                return null;
            }
            var bp = await context.BusinessPartner.Where(bp => bp.Id == user.BusinessPartner.Id).FirstOrDefaultAsync();
            return bp;
        }

        public async Task<ICollection<BusinessPartner>> GetAllGetAllBusinessPartner()
        {
            var bp = await context.BusinessPartner.ToListAsync();
            return bp;
        }

        public async Task<bool> UpdateBusinessPartner(BusinessPartner businessPartner)
        {
            context.BusinessPartner.Update(businessPartner);
            return await Save();
        }
    }
}
