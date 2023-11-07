using Bogus;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedBusinessPartner
    {
        public static async Task SeedBusinessPartnerAsync(ApplicationDbContext context)
        {
            Faker<BusinessPartner> faker = new Faker<BusinessPartner>();
            LinkedList<BusinessPartner> list = new LinkedList<BusinessPartner>();

            faker.RuleFor(bp => bp.PartnerName, f => f.Name.FullName());
            faker.RuleFor(bp => bp.Address, f => f.Address.FullAddress());
            faker.RuleFor(bp => bp.Email, f => f.Internet.Email());
            faker.RuleFor(bp => bp.PhoneNumber, f => f.Phone.PhoneNumber("##########"));

            for (int i = 1; i <= 3; i++)
            {
                BusinessPartner businessPartner = faker.Generate();
                list.AddLast(businessPartner);
            }
            await context.BusinessPartner.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
