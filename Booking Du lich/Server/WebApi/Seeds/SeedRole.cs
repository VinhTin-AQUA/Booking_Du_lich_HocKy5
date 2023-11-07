using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Seeds
{
    public static class SeedRole
    {
        public static readonly string ADMIN_ROLE = "Admin";
        public static readonly string EMPLOYEE_ROLE = "Employee";
        public static readonly string AGENTHOTEL_ROLE = "AgentHotel";
        public static readonly string AGENTTOUR_ROLE = "AgentTour";
        public static readonly string USER_ROLE = "User";

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.Roles.AnyAsync() == false)
            {
                await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
                await roleManager.CreateAsync(new IdentityRole(EMPLOYEE_ROLE));
                await roleManager.CreateAsync(new IdentityRole(AGENTHOTEL_ROLE));
                await roleManager.CreateAsync(new IdentityRole(AGENTTOUR_ROLE));
                await roleManager.CreateAsync(new IdentityRole(USER_ROLE));
            }
        }
    }
}
