using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Booking.Repositories;
using Booking.Seeds;
using WebApi.Seeds;

namespace Booking.Services
{
    public class SeedService
    {
        private readonly BookingContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public SeedService(
            BookingContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeContextAsync()
        {
            // kiểm tra có migration nào ở trạng thái pending không
            if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                // tiến hành cập nhật migration
                await context.Database.MigrateAsync();
            }

            if (await roleManager.Roles.AnyAsync() == false)
            {
                await SeedRole.SeedRolesAsync(roleManager);
            }

            if (await userManager.Users.AnyAsync() == false)
            {
                await SeedAgentTour.SeedAgentAsync(userManager); // tour
                await SeedEmployee.SeedEmployeeAsync(userManager); // employee
                await SeedAdmin.SeedAdminAsync(userManager); // admin
                await SeedUser.SeedUsersAsync(userManager); // user
            }

            if (await context.City.AnyAsync() == false)
            {
                await SeedCity.SeedCitiesAsync(context);
            }
        }
    }
}
