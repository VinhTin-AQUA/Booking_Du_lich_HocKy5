﻿using Booking.Models;

namespace Booking.Interfaces
{
    public interface IPackageRepository
    {
        public Task<bool> Save();
        public Task<bool> AddPackage(Package package);

        public Task<ICollection<Package>> GetAllPackages();

        public Task<ICollection<Package>> GetPackagesOfTour(int tourId);


        public Task<Package> GetPackageById(int packageId);

        public Task<ICollection<Package>> SearchPackageOfTour(int tourId);
        public Task<bool> DeletePackage(Package package);

        public Task<bool> UpdatePackage(Package package);
    }
}
