
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi1.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using WebApi.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using WebApi.DTOs.Authentication;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.MailService;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenRepository accountRepo;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;

        public AuthenticationController(IAuthenRepository repo, RoleManager<IdentityRole> role, IConfiguration configuration, IEmailSender emailSender)
        {
            accountRepo = repo;
            _roleManage = role;
            _configuration = configuration;
            this.emailSender = emailSender;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            try
            {
                if (signUpModel == null)
                {
                    return BadRequest(new JsonResult(new { title = "Error", message = "Error when sign-up." }));
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest();
                }

                //Check User exist
                var userExist = await accountRepo.GetUserByEmail(signUpModel.Email);

                if (userExist != null)
                {
                    return BadRequest(new JsonResult(new { title = "Error", message = "Email already taken. Please choose another email." }));
                }
                // Add user in the database
                var user = new ApplicationUser
                {
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    UserName = signUpModel.Email,
                    Email = signUpModel.Email,
                    Address = signUpModel.Address,
                };

                var result = await accountRepo.CreateUser(user, signUpModel.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(new JsonResult(new { title = "Error", message = "Register failed. Please try agian." }));
                }
                await accountRepo.AddRoleToUser(user, "Admin");

                if (await SendEmailConfirmAsync(user))
                {
                    return Ok(new JsonResult(new
                    {
                        Status = "Success",
                        Message = $"User created successfully and Send email to {user.Email}"
                    }));
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Something error. Please try again"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Register failed. Please try agian." }));
            }
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when login." }));
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var user = await accountRepo.GetUserByEmail(model.email);

            if (user.EmailConfirmed == true)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Your email has been confirmed." }));
            }

            if (user != null)
            {
                var result = await accountRepo.ConfirmEmail(user, model.token);
                if (result.Succeeded)
                {
                    return Ok(new JsonResult(new { title = "Success", message = "Email verified successfully." }));
                }
            }
            return BadRequest(new JsonResult(new { title = "Error", message = "User don't exist." }));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> SignIn(SignInModel signInModel)
        {
            if (signInModel == null || string.IsNullOrEmpty(signInModel.Email) || string.IsNullOrEmpty(signInModel.Password))
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password" }));
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password" }));
            }

            var user = await accountRepo.GetUserByEmail(signInModel.Email);
            if (user == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password." }));
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Please confirm your email." }));
            }

            var result = await accountRepo.CheckPassword(user, signInModel.Password);

            if (result.IsLockedOut)
            {
                return Unauthorized(string.Format("Your account has been locked. You should wait until {0} (UTC time)" +
                    "to be able to login", user.LockoutEnd));
            }

            if (result.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password." }));
            }
            var r = await accountRepo.CreateApplicationUserDto(user);
            return r;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await accountRepo.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value!);
            if (await accountRepo.IsLockedOut(user))
            {
                return Unauthorized(new JsonResult(new { title = "Error", message = "You have been lock out" }));
            }
            return await accountRepo.CreateApplicationUserDto(user!);
        }

        [HttpPost("resent-email")]
        public async Task<IActionResult> ResendEmail(ResendEmailConfirm model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var userExist = await accountRepo.GetUserByEmail(model.Email);

            if (userExist == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email." }));
            }

            if (userExist.EmailConfirmed)
            {
                return Ok(new JsonResult(new
                {
                    title = "Success",
                    message = "Your email has been confirmed."
                }));
            }

            if (await SendEmailConfirmAsync(userExist))
            {
                return Ok(new JsonResult(new
                {
                    title = "Success",
                    message = "Email has been resent."
                }));
            }

            return BadRequest(new JsonResult(new
            {
                title = "Error",
                message = $"Something error. Please try again."

            }));
        }


        [HttpPost("forgot-password/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error" }));
            }

            var userExisted = await accountRepo.GetUserByEmail(email);
            if (userExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Email is incorrect" }));
            }

            if (await SendForgotPasswordEmail(userExisted))
            {
                return Ok(new JsonResult(new { title = "Success", message = "Email sent" }));
            }
            return BadRequest(new JsonResult(new { title = "Error", message = "Email is incorrect" }));
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error" }));
            }

            var userExisted = await accountRepo.GetUserByEmail(model.Email);
            if (userExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "User is not existed" }));
            }
            // mayf cucs
            var result = await accountRepo.ResetPassword(userExisted, model.Token, model.Password);

            if(result.Succeeded == false) {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when reset password" }));
            }

            return Ok(new JsonResult(new { title = "Succes", message = "Reset password successfully" }));
        }



        // =============================================================================


        #region private method
        private async Task<bool> SendEmailConfirmAsync(ApplicationUser user)
        {
            var token = await accountRepo.GenerateEmailConfirmationToken(user);
            string url = $"{_configuration["JWT:UrlClient"]}/{_configuration["JWT:UrlConfirmEmail"]}?token={token}&email={user.Email}";

            Message message = new Message(new string[] { user.Email! },
                "Confirm Email",
                $"<p>We really happy when you using my app. Click <a href='{url}'>here</a> to verify email</p>"!);
            return await emailSender.SendEmail(message);
        }

        private async Task<bool> SendForgotPasswordEmail(ApplicationUser user)
        {
            var token = await accountRepo.GeneratePasswordResetToken(user);
            string url = $"{_configuration["JWT:UrlClient"]}/{_configuration["JWT:UrlResetPassword"]}?token={token}&email={user.Email}";

            Message message = new Message(new string[] { user.Email! },
                "Reset password",
                $"<p>To reset your password, please click <a href='{url}'>here</a></p>"!);
            return await emailSender.SendEmail(message);
        }
        #endregion
    }
}
