using Booking.Interfaces;
using Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace Booking.Repositories
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly UserManager<AppUser> _userManage;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> signInManager;

        public AuthenRepository(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManage = userManager;
            _configuration = configuration;
            this.signInManager = signInManager;
        }

        // login logout
        public async Task<IdentityResult> SignUp(AppUser model, string password)
        {
            return await _userManage.CreateAsync(model, password);
        }

        public async Task<AppUser> GetUserSignedIn(ClaimsPrincipal User)
        {
            var user = await _userManage.GetUserAsync(User);
            return user;
        }

        public async Task<SignInResult> Login(AppUser user, string password, bool rememberMe)
        {
            var result = await signInManager.PasswordSignInAsync(user.Email, password, true, false);
            return result;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        // accoubt
        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await _userManage.FindByEmailAsync(email);
            return user;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            var user = await _userManage.Users
                .Where(u => u.Id == id)
                .Include(u => u.BusinessPartner)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<IdentityResult> UpdateAccount(AppUser user)
        {
            var r = await _userManage.UpdateAsync(user);
            return r;
        }

        // password

        public async Task<SignInResult> CheckPassword(AppUser user, string password)
        {
            var r = await signInManager.CheckPasswordSignInAsync(user, password, false);
            return r;
        }

        public async Task<IdentityResult> ResetPassword(AppUser user, string token, string password)
        {
            var decodedToken = await _userManage.GeneratePasswordResetTokenAsync(user);
            decodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var resetResult = await _userManage.ResetPasswordAsync(user, decodedToken, password);
            return resetResult;
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            var r = await _userManage.ChangePasswordAsync(user, currentPassword, newPassword);
            return r;
        }

        //token
        public async Task<string> GenerateEmailConfirmationToken(AppUser user)
        {
            var token = await _userManage.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<string> GenerateResetPasswordToken(AppUser user)
        {
            var token = await _userManage.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<IdentityResult> ConfirmEmail(AppUser user, string token)
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var result = await _userManage.ConfirmEmailAsync(user, decodedToken);
            return result;
        }


        // role

        public async Task AddRoleToUser(AppUser user, string role)
        {
            var r = await _userManage.AddToRoleAsync(user, role);
        }

        public async Task<List<string>> GetRolesOfUser(AppUser user)
        {
            var userRoles = await _userManage.GetRolesAsync(user);
            return userRoles.ToList();
        }

        public async Task<bool> IsLockedOut(AppUser user)
        {
            var r = await _userManage.IsLockedOutAsync(user);
            return r;
        }

        public async Task<bool> CheckUserInRole(AppUser user, string role)
        {
            var r = await _userManage.IsInRoleAsync(user, role);
            return r;
        }
    }
}
