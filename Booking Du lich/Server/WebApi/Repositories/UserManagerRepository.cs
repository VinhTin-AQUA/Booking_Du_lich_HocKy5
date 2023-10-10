using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public int TotalUsers()
        {
            var totalUser = userManager.Users.Count();
            return totalUser;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUser(int currentPage, int pageSize)
        {
            var users = await userManager.Users
                .Skip(currentPage*pageSize)
                .Take(pageSize)
                .ToListAsync();
            return users;
        }

        public async Task<IdentityResult> LockUser(ApplicationUser user)
        {
            DateTimeOffset? date = new DateTimeOffset(DateTime.Now.AddDays(3));
            var r = await userManager.SetLockoutEndDateAsync(user, date);
            return r;
        } 

        public async Task<IdentityResult> UnlockUser(ApplicationUser user)
        {
            var r = await userManager.SetLockoutEndDateAsync(user, null);
            return r;
        }

        public async Task<IdentityResult> DeleteUser(ApplicationUser user)
        {
            var r = await userManager.DeleteAsync(user);
            return r;
        }
    }
}
