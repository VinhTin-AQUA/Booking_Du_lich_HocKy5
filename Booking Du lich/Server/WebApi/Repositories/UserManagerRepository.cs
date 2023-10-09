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


    }
}
