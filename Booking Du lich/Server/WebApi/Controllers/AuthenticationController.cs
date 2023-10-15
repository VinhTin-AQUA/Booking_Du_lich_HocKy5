
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
        private readonly IAuthenRepository authenRepository;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;

        public AuthenticationController(IAuthenRepository authenRepository, RoleManager<IdentityRole> role, IConfiguration configuration, IEmailSender emailSender)
        {
            this.authenRepository = authenRepository;
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
                    return BadRequest(ModelState);
                }

                //Check User exist
                var userExist = await authenRepository.GetUserByEmail(signUpModel.Email);

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
                    PhoneNumber = signUpModel.PhoneNumber,
                };

                var result = await authenRepository.CreateUser(user, signUpModel.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(new JsonResult(new { title = "Error", message = "Register failed. Please try agian." }));
                }
                await authenRepository.AddRoleToUser(user, "User");

                if (await  emailSender.SendEmailConfirmAsync(user))
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

            var user = await authenRepository.GetUserByEmail(model.email);

            if (user.EmailConfirmed == true)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Your email has been confirmed." }));
            }

            if (user != null)
            {
                var result = await authenRepository.ConfirmEmail(user, model.token);
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

            var user = await authenRepository.GetUserByEmail(signInModel.Email);
            if (user == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password." }));
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Please confirm your email." }));
            }
            
            if(user.LockoutEnd != null)
            {
                return BadRequest(new JsonResult(new { title = "locked", message = $"Your account has been locked. Please login after {user.LockoutEnd}" }));
            }

            var result = await authenRepository.CheckPassword(user, signInModel.Password);

            if (result.IsLockedOut)
            {
                return Unauthorized(string.Format("Your account has been locked. You should wait until {0} (UTC time)" +
                    "to be able to login", user.LockoutEnd));
            }

            if (result.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Incorrect email or password." }));
            }
            var r = await authenRepository.CreateApplicationUserDto(user);
            return r;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await authenRepository.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value!);
            if (await authenRepository.IsLockedOut(user))
            {
                return Unauthorized(new JsonResult(new { title = "Error", message = "You have been lock out" }));
            }
            return await authenRepository.CreateApplicationUserDto(user!);
        }

        [HttpPost("resent-email")]
        public async Task<IActionResult> ResendEmail(ResendEmailConfirm model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var userExist = await authenRepository.GetUserByEmail(model.Email);

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

            if (await emailSender.SendEmailConfirmAsync(userExist))
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

            var userExisted = await authenRepository.GetUserByEmail(email);
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

            var userExisted = await authenRepository.GetUserByEmail(model.Email);
            if (userExisted == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "User is not existed" }));
            }
            // mayf cucs
            var result = await authenRepository.ResetPassword(userExisted, model.Token, model.Password);

            if(result.Succeeded == false) {
                return BadRequest(new JsonResult(new { title = "Error", message = "Error when reset password" }));
            }

            return Ok(new JsonResult(new { title = "Succes", message = "Reset password successfully" }));
        }



        // =============================================================================


        #region private method
        private async Task<bool> SendForgotPasswordEmail(ApplicationUser user)
        {
            var token = await authenRepository.GeneratePasswordResetToken(user);
            string url = $"{_configuration["JWT:UrlClient"]}/{_configuration["JWT:UrlResetPassword"]}?token={token}&email={user.Email}";

            Message message = new Message(new string[] { user.Email! },
                "Reset password",
                $"<p>To reset your password, please click <a href='{url}'>here</a></p>"!);
            return await emailSender.SendEmail(message);
        }
        #endregion
    }
}
