using Bogus;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedPackage
    {
        public static async Task SeedPackageAsync(ApplicationDbContext context)
        {
            Faker<Package> fk = new Faker<Package>();
            LinkedList<Package> packages = new LinkedList<Package>();

            fk.RuleFor(p => p.TourID, f => f.Random.Int(1, 30));
            fk.RuleFor(p => p.PackageName, f => f.Lorem.Sentence(2,4));
            fk.RuleFor(p => p.Decription, f => f.Lorem.Sentence(5,7));
            fk.RuleFor(p => p.MaxPeople, f => f.Random.Int(1,10));

            for (int i = 1; i<=10; i++)
            {
                Package p = fk.Generate();
                packages.AddLast(p);    
            }
            await context.Packages.AddRangeAsync(packages);
            await context.SaveChangesAsync();
        }
    }
}
