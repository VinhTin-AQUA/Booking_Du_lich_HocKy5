using Microsoft.AspNetCore.Identity;
using WebApi.DTOs.Authentication;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly IConfiguration _configuration;
        private readonly JWTService jwtService;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration,
            JWTService jwtService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManager;
            _configuration = configuration;
            this.jwtService = jwtService;
            this.roleManager = roleManager;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await _userManage.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> IsLockedOut(ApplicationUser user)
        {
            var r = await _userManage.IsLockedOutAsync(user);
            return r;
        }

        public async Task<UserDto> CreateApplicationUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = await jwtService.CreateJWT(user)
            };
        }
    }
}
