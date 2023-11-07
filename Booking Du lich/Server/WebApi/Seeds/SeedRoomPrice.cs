using Bogus;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedRoomPrice
    {
        public static async Task SeedRoomPriceAsync(ApplicationDbContext context)
        {
            Faker<RoomPrice> fk = new Faker<RoomPrice>();
            LinkedList<RoomPrice> list = new LinkedList<RoomPrice>();

            fk.RuleFor(rp => rp.Price, f => f.Random.Double(100,999));
            fk.RuleFor(rp => rp.ValidFrom, f => f.Date.Past());
            fk.RuleFor(rp => rp.GoodThru, f => f.Date.Future());


            for (int i = 1; i<=50; i++)
            {
                RoomPrice price = fk.Generate();
                price.RoomId = i;
                list.AddLast(price);
            }
            await context.RoomPrices.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
