using Bogus;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedTourType
    {
        public static async Task SeedTourTypes(ApplicationDbContext context)
        {
            LinkedList<TourType> list = new LinkedList<TourType>();
            Faker<TourType> fk = new Faker<TourType>();
            fk.RuleFor(tt => tt.TourTypeName, f => f.Lorem.Sentence(2,5));

            for (int i = 1;  i <= 15; i++)
            {
                TourType tt = fk.Generate();
                list.AddLast(tt);
            }
            await context.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
