using Booking.Interfaces;
using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<AppUser> userManager;

        public UserManagerRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public int TotalUsers(string searchString = "")
        {
            int totalUser = 0;
            if (string.IsNullOrEmpty(searchString))
            {
                totalUser = userManager.Users.Where(u => u.Email != SeedAdmin.Email).Count();
                return totalUser;
            }

            totalUser = userManager.Users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Count();
            return totalUser;
        }

        public async Task<IEnumerable<AppUser>> GetUsers(int currentPage, int pageSize, string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var users = await userManager.Users
                .Where(u => u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();
                return users;
            }

            var _users = await userManager.Users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)

                .ToListAsync();
            return _users;
        }

        public async Task<AppUser?> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var user = await userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IdentityResult> LockUser(AppUser user)
        {
            DateTimeOffset? date = new DateTimeOffset(DateTime.Now.AddDays(3));
            var r = await userManager.SetLockoutEndDateAsync(user, date);
            return r;
        }

        public async Task<IdentityResult> UnlockUser(AppUser user)
        {
            var r = await userManager.SetLockoutEndDateAsync(user, null);
            return r;
        }

        public async Task<IdentityResult> DeleteUser(AppUser user)
        {
            var r = await userManager.DeleteAsync(user);
            return r;
        }
    }
}
