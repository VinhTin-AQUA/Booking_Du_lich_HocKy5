
using Booking.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Booking.Seeds
{
    public static class SeedEmployee
    {
        public static readonly string Email = "employee@gmail.com";
        public static readonly string FirstName = "";
        public static readonly string LastName = "Employee";
        public static readonly string Password = "employee123";
        public static readonly string Address = "Khanh Hoa";
        public static readonly string Phone = "0123456789";

        public static async Task SeedEmployeeAsync(UserManager<AppUser> userManager)
        {
            var employee = new AppUser()
            {
                UserName = Email,
                Email = Email,
                EmailConfirmed = true,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
            };

            await userManager.CreateAsync(employee, Password);

            await userManager.AddToRolesAsync(employee, new[] { SeedRole.EMPLOYEE_ROLE });
            await userManager.AddClaimsAsync(employee, new Claim[]
            {
        new Claim(ClaimTypes.Email, employee.Email),
            });
        }
    }
}
