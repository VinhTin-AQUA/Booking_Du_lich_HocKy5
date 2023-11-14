using Booking.Models;
using Microsoft.AspNetCore.Identity;

namespace Booking.Interfaces
{
    public interface IBusinessPartnerRepository
    {
        public int TotalPartners(string searchString = "");

        public Task<bool> Save();
        public Task<bool> AddBusinessPartner(BusinessPartner businessPartner);

        public Task<bool> DeleteAgents(BusinessPartner partner);
        public Task<ICollection<AppUser>> GetAgentsPartner(int partnerId);

        public Task<bool> DeleteBusinessPartner(BusinessPartner businessPartner);
        public Task<BusinessPartner?> GetBusinessPartnerById(int? id);
        public Task<ICollection<BusinessPartner>?> GetAllBusinessPartner(int currentPage, int pageSize, string? searchString);
        public Task<bool> UpdateBusinessPartner(BusinessPartner businessPartner);
    }
}
