using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Models;
using WebApi.Seeds;
using WebApi1.Data;

namespace WebApi.Services
{
    public class SeedService
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
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

            await SeedRoles();
            await SeedAdmin();
        }

        private async Task SeedRoles()
        {
            
            if (await roleManager.Roles.AnyAsync() == false)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.ADMIN_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.EMPLOYEE_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.AGENT_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.USER_ROLE));


                


            }
        }

        private async Task SeedAdmin()
        {
            // seed user
            if (await userManager.Users.AnyAsync() == false)
            {
                // admin
                var admin = new ApplicationUser()
                {
                    UserName = AdminAccount.AdminEmail,
                    Email = AdminAccount.AdminEmail,
                    EmailConfirmed = true,
                    FirstName = AdminAccount.AdminFirstName,
                    LastName = AdminAccount.AdminLastName,
                    Address = AdminAccount.AdminAddress,
                };

                await userManager.CreateAsync(admin, AdminAccount.AdminPassword);

                await userManager.AddToRolesAsync(admin, new[] { RoleSeed.ADMIN_ROLE });
                await userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                });
            }
        }
    }
}
