using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserManagerRepository
    {
        public Task<IEnumerable<ApplicationUser>> GetUsers(int currentPage, int pageSize, string? searchString);

        public int TotalUsers();
        public Task<IdentityResult> LockUser(ApplicationUser user);
        public Task<IdentityResult> UnlockUser(ApplicationUser user);
        public Task<IdentityResult> DeleteUser(ApplicationUser user);
    }
}
