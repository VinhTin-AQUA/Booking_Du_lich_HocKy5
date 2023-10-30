﻿using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class PackagePriceRepository : IPackagePriceRepository
    {
        private readonly ApplicationDbContext context;

        public PackagePriceRepository(ApplicationDbContext context) { this.context = context; }
        public async Task<bool> AddPackPrice(PackagePrice packagePrice)
        {
            context.PackagePrices.Add(packagePrice);
            return await Save();
        }

        public async Task<bool> DeletePackagePrice(PackagePrice PackagePrice)
        {
            if (PackagePrice == null)
            {
                return false;
            }
            context.PackagePrices.Remove(PackagePrice);
            return await Save();
        }

        public async Task<ICollection<PackagePrice>> GetAllPackagePrices()
        {
            var packagePrices = await context.PackagePrices.ToListAsync();
            return packagePrices;
        }

        public async Task<PackagePrice> GetPackagePriceById(int? id)
        {
            var packagePrice = await context.PackagePrices
                  .Where(pp => pp.PackageId == id)
                  .FirstOrDefaultAsync();
            return packagePrice;
        }

        public async Task<PackagePrice> GetPackagePriceByID(int? id, DateTime? validFrom)
        {
            var packagePrice = await context.PackagePrices
                 .Where(pp => pp.PackageId == id && pp.ValidFrom == validFrom)
                 .FirstOrDefaultAsync();
            return packagePrice;
        }

        public async Task<PackagePrice> GetPackagePriceByPrice(double price)
        {
            var packagePrice = await context.PackagePrices
                .Where(pp => pp.Price == price)
                .FirstOrDefaultAsync();
            return packagePrice;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdatePackagePrice(PackagePrice PackagePrice)
        {
            context.PackagePrices.Update(PackagePrice);
            return await Save();
        }
    }
}
