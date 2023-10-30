using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IBusinessPartnerRepository
    {
        public Task<bool> Save();
        public Task<bool> AddBusinessPartner(BusinessPartner businessPartner);
        
        public Task<bool> DeleteBusinessPartner(BusinessPartner businessPartner);
        public Task<BusinessPartner> GetBusinessPartnerById(int? id);
        public Task<ICollection<BusinessPartner>> GetAllGetAllBusinessPartner();
        public Task<bool> UpdateBusinessPartner(BusinessPartner businessPartner);
    }
}
