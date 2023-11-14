using Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace Booking.Interfaces
{
    public interface IAuthenRepository
    {
        // login logout
        public Task<IdentityResult> SignUp(AppUser model, string password);

        public Task<AppUser> GetUserSignedIn(ClaimsPrincipal User);

        public Task<SignInResult> Login(AppUser user, string password, bool rememberMe);

        public Task Logout();

        // account
        public Task<AppUser> GetUserByEmail(string email);

        public Task<AppUser> GetUserById(string id);
        public Task<IdentityResult> UpdateAccount(AppUser user);

        // password

        public Task<SignInResult> CheckPassword(AppUser user, string password);

        public Task<IdentityResult> ResetPassword(AppUser user, string token, string password);

        public Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword);

        //token
        public Task<string> GenerateEmailConfirmationToken(AppUser user);

        public Task<string> GenerateResetPasswordToken(AppUser user);

        public Task<IdentityResult> ConfirmEmail(AppUser user, string token);


        // role

        public Task AddRoleToUser(AppUser user, string role);

        public Task<List<string>> GetRolesOfUser(AppUser user);

        public Task<bool> IsLockedOut(AppUser user);

        public Task<bool> CheckUserInRole(AppUser user, string role);
    }
}
