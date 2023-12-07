using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly BookingContext context;

        public PackageRepository(BookingContext context) { this.context = context; }
        public async Task<bool> AddPackage(Package package)
        {
            context.Packages.Add(package);
            return await Save();
        }

        public async Task<bool> DeletePackage(Package package)
        {
            if (package == null)
            {
                return false;
            }
            context.Packages.Remove(package);
            return await Save();
        }

        public async Task<ICollection<Package>> GetAllPackages()
        {
            var packages = await context.Packages.ToListAsync();
            return packages;
        }

        public async Task<ICollection<Package>> GetPackagesOfTour(int tourId)
        {
            var packages = await context.Packages
                .Where(p => p.TourID == tourId)
                .Include(p => p.PackagePrices)
                .ToListAsync();
            return packages;
        }

        public async Task<Package> GetPackageById(int packageId)
        {
            var package = await context.Packages
               .Where(p => p.PackageID == packageId)
               .Include(p => p.PackagePrices)
               .FirstOrDefaultAsync();
            return package;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<ICollection<Package>> SearchPackageOfTour(int tourId)
        {
            var packages = await context.Packages
                .Where(p => p.TourID == tourId)
                .ToListAsync();
            return packages;
        }

        public async Task<bool> UpdatePackage(Package package)
        {
            context.Packages.Update(package);
            return await Save();
        }
    }
}
