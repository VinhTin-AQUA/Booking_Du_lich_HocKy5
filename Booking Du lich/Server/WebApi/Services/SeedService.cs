using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Seeds;

namespace WebApi.Services
{
    public class SeedService
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedService(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeContextAsync()
        {
            string[] agentHotelIds = null;
            string[] agentTourIds = null;
            string[] employeeIds = null;

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

            if (await context.BusinessPartner.AnyAsync() == false)
            {
                await SeedBusinessPartner.SeedBusinessPartnerAsync(context);
            }

            if (await userManager.Users.AnyAsync() == false)
            {
                agentHotelIds = await SeedAgentHotel.SeedAgentAsync(userManager); //   
                agentTourIds = await SeedAgentTour.SeedAgentAsync(userManager); // 
                employeeIds = await SeedEmployee.SeedEmployeeAsync(userManager); // 

                await SeedAdmin.SeedAdminAsync(userManager);
                await SeedUser.SeedUsersAsync(userManager);
            }

            if (await context.City.AnyAsync() == false)
            {
                await SeedCity.SeedCitiesAsync(context);
            }

            if (await context.TourType.AnyAsync() == false)
            {
                await SeedTourType.SeedTourTypes(context);
            }

            if (await context.Tour.AnyAsync() == false)
            {
                await SeedTour.SeedTours(
                    context,
                    agentTourIds,
                    employeeIds);
            }

            if (await context.Packages.AnyAsync() == false)
            {
                await SeedPackage.SeedPackageAsync(context);
            }

            if (await context.PackagePrices.AnyAsync() == false)
            {
                await SeedPackagePrice.SeedPackagePriceAsync(context);
            }

            if (await context.HotelService.AnyAsync() == false)
            {
                await SeedHotelService.SeedHotelServiceAsync(context);
            }

            if (await context.Hotel.AnyAsync() == false)
            {
                await SeedHotel.SeedHotelAsync(context, agentHotelIds, employeeIds);
            }

            if(await context.HasServices.AnyAsync() == false)
            {
                await SeedHasService.SeedHasServiceAsync(context);
            }

            if (await context.Room.AnyAsync() == false)
            {
                await SeedRoom.SeedRoomAsync(context);
            }

            if (await context.RoomPrices.AnyAsync() == false)
            {
                await SeedRoomPrice.SeedRoomPriceAsync(context);
            }
        }
    }
}
