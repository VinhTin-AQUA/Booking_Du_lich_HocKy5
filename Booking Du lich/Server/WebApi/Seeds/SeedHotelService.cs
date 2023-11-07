using Bogus;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedHotelService
    {
        public static async Task SeedHotelServiceAsync(ApplicationDbContext context)
        {
            Faker<HotelService> fk = new Faker<HotelService>();
            LinkedList<HotelService> list = new LinkedList<HotelService>();
            fk.RuleFor(s => s.ServiceName, f => f.Lorem.Sentence(4));

            for(int i = 1; i<=10; i++)
            {
                HotelService hotelService = fk.Generate();
                list.AddLast(hotelService);
            }
            await context.HotelService.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
