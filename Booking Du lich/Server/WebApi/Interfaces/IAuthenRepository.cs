using Microsoft.AspNetCore.Identity;
using WebApi.DTOs.Authentication;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IAuthenRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        //public Task<IdentityResult> SignInAsync(Sign)
        
        public Task<IdentityResult> CreateUser(ApplicationUser user, string password);
        public Task AddRoleToUser(ApplicationUser user, string role);
        public Task<IdentityResult> ConfirmEmail(ApplicationUser user, string token);
        public  Task<string> GenerateEmailConfirmationToken(ApplicationUser user);
        public Task<SignInResult> CheckPassword(ApplicationUser user, string password);
        public Task<List<string>> GetRolesOfUser(ApplicationUser user);
        public  Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, ResetPassword resetPassword);
        public Task<string> GeneratePasswordResetToken(ApplicationUser user);
        public Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string newPassword);
        public Task<ApplicationUser> GetUserByEmail(string email);
        public Task<ApplicationUser> GetUserById(string id);
        public Task<bool> IsLockedOut(ApplicationUser user);
        public Task<UserDto> CreateApplicationUserDto(ApplicationUser user);
        public Task<IdentityResult> UpdateAccount(ApplicationUser user);
    }
}
