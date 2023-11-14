using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Booking.Models;

namespace Booking.Seeds
{
    public static class SeedAdmin
    {
        public static readonly string Email = "tinhovinh@gmail.com";
        public static readonly string FirstName = "";
        public static readonly string LastName = "Admin";
        public static readonly string Password = "admin123";
        public static readonly string Address = "Khanh Hoa";
        public static readonly string Phone = "0123456789";

        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var admin = new AppUser()
            {
                UserName = Email,
                Email = Email,
                EmailConfirmed = true,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
            };

            await userManager.CreateAsync(admin, Password);

            await userManager.AddToRolesAsync(admin, new[] { SeedRole.ADMIN_ROLE });
            await userManager.AddClaimsAsync(admin, new Claim[]
            {
                    new Claim(ClaimTypes.Email, admin.Email),
            });
        }
    }
}
