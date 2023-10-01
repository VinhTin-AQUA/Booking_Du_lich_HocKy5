
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

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;
        private readonly IUserRepository userRepository;

        public AuthenticationController(IAccountRepository repo, RoleManager<IdentityRole> role, IConfiguration configuration, IEmailSender emailSender, IUserRepository userRepository)
        {
            accountRepo = repo;
            _roleManage = role;
            _configuration = configuration;
            this.emailSender = emailSender;
            this.userRepository = userRepository;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            try
            {
                //Check User exist
                var userExist = await userRepository.GetUserByEmail(signUpModel.Email);

                if (userExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "Email already taken. Please choose another email!" });
                }
                // Add user in the database
                var user = new ApplicationUser
                {
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    UserName = signUpModel.Email,
                    Email = signUpModel.Email,
                };

                var result = await accountRepo.CreateUser(user, signUpModel.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "Register failed. Please try agian."
                    });
                }
                await accountRepo.AddRoleToUser(user, "User");

                if (await SendEmailConfirmAsync(user))
                {
                    return StatusCode(StatusCodes.Status200OK, new Response
                    {
                        Status = "Success",
                        Message = $"User created successfully and Send email to {user.Email}"
                    });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Something error. Please try again"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status416RangeNotSatisfiable, ex);
            }
        }

        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            var user = await userRepository.GetUserByEmail(model.email);
            if (user != null)
            {
                var result = await accountRepo.ConfirmEmail(user, model.token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Email verified successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User don't exist" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> SignIn(SignInModel signInModel)
        {
            if (signInModel == null || string.IsNullOrEmpty(signInModel.Email) || string.IsNullOrEmpty(signInModel.Password))
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Account is invalid" }));
            }

            var user = await userRepository.GetUserByEmail(signInModel.Email);
            if (user == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Email or password is incorrect." }));
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
                return BadRequest(new JsonResult(new { title = "Error", message = "Please confirm your email." }));
            }
            var r = await userRepository.CreateApplicationUserDto(user);
            return r;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await userRepository.GetUserByEmail(User.FindFirst(ClaimTypes.Email)?.Value!);
            if (await userRepository.IsLockedOut(user))
            {
                return Unauthorized(new JsonResult(new { title = "Error", message = "You have been lock out" }));
            }
            return await userRepository.CreateApplicationUserDto(user!);
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


        #endregion
    }
}
