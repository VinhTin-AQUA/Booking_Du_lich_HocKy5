using WebApi.DTOs.Authentication;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser> GetUserByEmail(string email);
        public Task<bool> IsLockedOut(ApplicationUser user);
        public Task<UserDto> CreateApplicationUserDto(ApplicationUser user);
    }
}
