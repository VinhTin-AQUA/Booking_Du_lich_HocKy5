using Bogus;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedHasService
    {
        public async static Task SeedHasServiceAsync(ApplicationDbContext context)
        {
            LinkedList<HasService> list = new LinkedList<HasService>();


            for (int i = 1; i<= 10; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    HasService hs = new HasService();
                    hs.HotelID = i;
                    hs.ServiceID = i > 8 ? j + 0  : j + 5;
                    list.AddLast(hs);
                }
            }

            await context.HasServices.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
