using Bogus;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedPackagePrice
    {
        public static async Task SeedPackagePriceAsync(ApplicationDbContext context)
        {
            Faker<PackagePrice> fk = new Faker<PackagePrice>();
            LinkedList<PackagePrice> list = new LinkedList<PackagePrice>();

            fk.RuleFor(t => t.Price, f => f.Random.Double(100, 200)); 
            fk.RuleFor(t => t.ValidFrom, f => f.Date.Past()); 
            fk.RuleFor(t => t.GoodThru, f => f.Date.Future());
            fk.RuleFor(t => t.PackageId, f => f.Random.Int(1,10));

            for (int i = 1; i<=10; i++)
            {
                PackagePrice pr = fk.Generate();
                list.AddLast(pr);
            }
            await context.PackagePrices.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
