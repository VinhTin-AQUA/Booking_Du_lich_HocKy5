
using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
