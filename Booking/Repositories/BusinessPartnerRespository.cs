using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security;
using Booking.Models;
using Booking.Data;
using Booking.Interfaces;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;

namespace Booking.Repositories
{
    public class BusinessPartnerRespository : IBusinessPartnerRepository
    {
        private readonly BookingContext context;
        private readonly UserManager<AppUser> userManager;

        public BusinessPartnerRespository(BookingContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        public int TotalPartners(string searchString = "")
        {
            int totalUser = 0;
            if (string.IsNullOrEmpty(searchString))
            {
                totalUser = context.BusinessPartner.Where(u => u.Email != SeedAdmin.Email).Count();
                return totalUser;
            }

            totalUser = context.BusinessPartner
                .Where(u => (u.PartnerName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Count();
            return totalUser;
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

		public async Task<bool> DeleteAgents(BusinessPartner partner)
        {
			var agents = await GetAgentsPartner(partner.Id);
            context.Users.RemoveRange(agents);
            return await Save();
		}

        public async Task<ICollection<AppUser>> GetAgentsPartner(int partnerId)
        {
            var users = await userManager.Users
                .Include(u => u.BusinessPartner)
                .Where(u => u.PartnerId == partnerId)
                .ToListAsync();

            return users;
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

        public async Task<BusinessPartner?> GetBusinessPartnerById(int? id)
        {
            var bp = await context.BusinessPartner
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return bp;
        }


        public async Task<ICollection<BusinessPartner>?> GetAllBusinessPartner(int currentPage, int pageSize, string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var users = await context.BusinessPartner
                .Where(u => u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();
                return users;
            }

            var _users = await context.BusinessPartner
                .Where(u => (u.PartnerName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)

                .ToListAsync();
            return _users;


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
