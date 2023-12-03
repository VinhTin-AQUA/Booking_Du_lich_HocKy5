
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Booking.Models;
using Booking.Data;
using Bogus;
using Booking.Seeds;

namespace WebApi.Seeds
{
    public class SeedAgentTour
    {
        public static readonly string Email = "agent_tour@gmail.com";
        public static readonly string FirstName = "Đối tác";
        public static readonly string LastName = "tour";
        public static readonly string Password = "agent123";
        public static readonly string Address = "Khanh Hoa";
        public static readonly string Phone = "0123456789";

        public static async Task SeedAgentAsync(UserManager<AppUser> userManager)
        {
            //string fakePassword = "default123";
            //var passwordHasher = new PasswordHasher<IdentityUser>();
            //string hashedPassword = passwordHasher.HashPassword(null, fakePassword);

            //Faker<AppUser> fk = new Faker<AppUser>();

            //fk.RuleFor(u => u.FirstName, f => f.Name.FirstName());
            //fk.RuleFor(u => u.LastName, f => f.Name.LastName());
            //fk.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
            //fk.RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper());
            //fk.RuleFor(u => u.EmailConfirmed, f => true);
            //fk.RuleFor(u => u.UserName, (f, u) => u.Email);
            //fk.RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper());
            //fk.RuleFor(u => u.PasswordHash, f => hashedPassword);
            //fk.RuleFor(u => u.Address, f => f.Address.FullAddress());
            //fk.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));

            //for (int i = 1; i <= 5; i++)
            //{
            //    var agentTour = fk.Generate();

            //    await userManager.CreateAsync(agentTour);

            //    await userManager.AddToRolesAsync(agentTour, new[] { SeedRole.AGENTTOUR_ROLE });
            //    await userManager.AddClaimsAsync(agentTour, new Claim[]
            //    {
            //        new Claim(ClaimTypes.Email, agentTour.Email),
            //    });
            //}

			var agentTour = new AppUser()
			{
				UserName = Email,
				Email = Email,
				EmailConfirmed = true,
				FirstName = FirstName,
				LastName = LastName,
				Address = Address,
			};

			await userManager.CreateAsync(agentTour, Password);

			await userManager.AddToRolesAsync(agentTour, new[] { SeedRole.AGENTTOUR_ROLE });
			await userManager.AddClaimsAsync(agentTour, new Claim[]
			{
					new Claim(ClaimTypes.Email, agentTour.Email),
			});
		}
    }
}
