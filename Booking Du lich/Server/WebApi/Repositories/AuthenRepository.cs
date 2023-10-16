﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApi.DTOs.Authentication;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Services;

namespace WebApi1.Repositories
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JWTService jwtService;

        public AuthenRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, JWTService jwtService)
        {
            _userManage = userManager;
            _configuration = configuration;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }


        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
            };

            //await _userManage.AddToRoleAsync(user, role);
            //var token = await _userManage.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail))



            return await _userManage.CreateAsync(user, model.Password);
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user, string password)
        {
            var result = await _userManage.CreateAsync(user, password);
            return result;
        }

        public async Task AddRoleToUser(ApplicationUser user, string role)
        {
            var r = await _userManage.AddToRoleAsync(user, role);
        }

        public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
        {
            var token = await _userManage.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManage.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<IdentityResult> ConfirmEmail(ApplicationUser user, string token)
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var result = await _userManage.ConfirmEmailAsync(user, decodedToken);
            return result;
        }

        public async Task<SignInResult> CheckPassword(ApplicationUser user, string password)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }

        public async Task<List<string>> GetRolesOfUser(ApplicationUser user)
        {
            var userRoles = await _userManage.GetRolesAsync(user);
            return userRoles.ToList();
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, ResetPassword resetPassword)
        {
            var resetResult = await _userManage.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            return resetResult;
        }

        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            var token = await _userManage.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string newPassword)
        {
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var r = await _userManage.ResetPasswordAsync(user, decodedToken, newPassword);
            return r;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await _userManage.FindByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userManage.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
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