using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Booking.Models;
using Booking.Data;

namespace Booking.Seeds
{
    public static class SeedUser
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
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
            fk.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));
            fk.RuleFor(u => u.PasswordHash, f => hashedPassword);
            fk.RuleFor(u => u.Address, f => f.Address.FullAddress());

            for (int i = 1; i <= 500; i++)
            {
                AppUser user = fk.Generate();

                await userManager.CreateAsync(user);

                await userManager.AddToRolesAsync(user, new[] { SeedRole.USER_ROLE });
                await userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                });
            }
        }
    }
}
