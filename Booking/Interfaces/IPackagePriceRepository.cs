using Booking.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Booking.Interfaces
{
    public interface IPackagePriceRepository
    {
        public Task<bool> Save();
        public Task<bool> AddPackPrice(PackagePrice packagePrice);

        public Task<ICollection<PackagePrice>> GetAllPackagePrices();
        //public Task<PackagePrice> GetPackagePriceByPrice(double price);

        public Task<ICollection<PackagePrice>> GetPackagePricesOfPackge(int packageId);

        public Task<PackagePrice> GetPackagePriceById(int? id);
        public Task<PackagePrice> GetPackagePriceByID(int? id, DateTime? validFrom);

        public Task<bool> DeletePackagePrice(PackagePrice PackagePrice);


        //public Task<IEnumerable<PackagePrice>> SearchPackageType(decimal price);

        public Task<bool> UpdatePackagePrice(PackagePrice PackagePrice);
    }
}
