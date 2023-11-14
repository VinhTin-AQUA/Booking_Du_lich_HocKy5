using Booking.Models;
using Microsoft.AspNetCore.Identity;

namespace Booking.Interfaces
{
    public interface IUserManagerRepository
    {
        public Task<IEnumerable<AppUser>> GetUsers(int currentPage, int pageSize, string? searchString);
        public Task<AppUser?> GetUserById(string userId);
        public int TotalUsers(string searchString = "");
        public Task<IdentityResult> LockUser(AppUser user);
        public Task<IdentityResult> UnlockUser(AppUser user);
        public Task<IdentityResult> DeleteUser(AppUser user);
    }
}
