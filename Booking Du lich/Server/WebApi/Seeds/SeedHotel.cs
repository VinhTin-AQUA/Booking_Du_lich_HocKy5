using Bogus;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedHotel
    {
        public static async Task SeedHotelAsync(ApplicationDbContext context, string[] agentHotelIds, string[] employeeIds)
        {
            LinkedList<Hotel> list = new LinkedList<Hotel>();
            Faker<Hotel> fk = new Faker<Hotel>();
            fk.RuleFor(h => h.HotelName, f => f.Company.CompanyName());
            fk.RuleFor(h => h.Address, f => f.Address.FullAddress());
            fk.RuleFor(h => h.Description, f => f.Lorem.Paragraph());
            fk.RuleFor(h => h.PostingDate, f => f.Date.Past());
            fk.RuleFor(h => h.ApprovalDate, f => f.Date.Future());
            fk.RuleFor(h => h.PosterID, f => agentHotelIds[f.Random.Int(0, 4)]);
            fk.RuleFor(h => h.ApproverID, f => employeeIds[f.Random.Int(0, 4)]);
            fk.RuleFor(h => h.CityId, f => f.Random.Int(1, 63));

            // photo path
            for (int i = 1; i <= 10; i++)
            {
                Hotel hotel = fk.Generate();

                hotel.PhotoPath = $"/hotels/{i}/_imgHotel";

                list.AddLast(hotel);
            }
            await context.Hotel.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
