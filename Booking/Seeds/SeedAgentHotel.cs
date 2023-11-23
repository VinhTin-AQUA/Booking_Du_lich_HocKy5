using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Booking.Models;
using Booking.Data;
using Booking.Seeds;

namespace WebApi.Seeds
{
    public class SeedAgentHotel
    {
        public static async Task SeedAgentAsync(UserManager<AppUser> userManager)
        {
            string fakePassword = "default123";
            var passwordHasher = new PasswordHasher<IdentityUser>();
            string hashedPassword = passwordHasher.HashPassword(null, fakePassword);

            Faker<AppUser> fk = new Faker<AppUser>();

            fk.RuleFor(u => u.FirstName, f => f.Name.FirstName());
            fk.RuleFor(u => u.LastName, f => f.Name.LastName());
            fk.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
            fk.RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper());
            fk.RuleFor(u => u.EmailConfirmed, f => true);
            fk.RuleFor(u => u.UserName, (f, u) => u.Email);
            fk.RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper());
            fk.RuleFor(u => u.PasswordHash, f => hashedPassword);
            fk.RuleFor(u => u.Address, f => f.Address.FullAddress());
            fk.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));
            fk.RuleFor(u => u.PartnerId, f => f.Random.Int(1, 3));

            for (int i = 1; i <= 5; i++)
            {
                var agentHotel = fk.Generate();
                await userManager.CreateAsync(agentHotel);
                await userManager.AddToRolesAsync(agentHotel, new[] { SeedRole.AGENTHOTEL_ROLE });
                await userManager.AddClaimsAsync(agentHotel, new Claim[]
                {
                    new Claim(ClaimTypes.Email, agentHotel.Email),
                });
            }
        }
    }
}
