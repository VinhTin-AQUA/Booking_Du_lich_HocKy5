using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ApplicationDbContext context;

        public PackageRepository(ApplicationDbContext context) { this.context = context; }
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

        public async Task<Package> GetPackageById(int packageId)
        {
            var package = await context.Packages
               .Where(p => p.PackageID == packageId )
               .FirstOrDefaultAsync();
            return package;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<ICollection<Package>> SearchPackageByMaxPeople(int maxPeople)
        {
            var packages = await context.Packages
                .Where(p => p.MaxPeople == maxPeople)
                .ToListAsync();
            return packages;
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
