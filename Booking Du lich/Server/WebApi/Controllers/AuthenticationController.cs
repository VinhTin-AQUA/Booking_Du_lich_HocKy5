
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using User.Management.Service.Service;
using WebApi1.Data;
using WebApi1.Models;
using WebApi1.Models.Authentication.SignIn;
using WebApi1.Models.Authentication.SignUp;
using WebApi1.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using User.Management.Service.Models;
using WebApi.Models.Authentication.SignUp;
using System.Globalization;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _loginManage;

        public AuthenticationController(IAccountRepository repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role, IConfiguration configuration, IEmailService emailService, SignInManager<ApplicationUser> loginManage) {
            accountRepo = repo;
            _userManage = userManager;
            _roleManage = role;
            _configuration = configuration;
            _emailService = emailService;
            _loginManage = loginManage;
        }

       
        [HttpPost("SignUp")]

        public async Task<IActionResult> SignUp(SignUpModel signUpModel, string role)
        {
            try
            {
                //Check User exist
                var userExist = await _userManage.FindByEmailAsync(signUpModel.Email);
                if (userExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exist!" });
                }
                // Add user in the database
                var user = new ApplicationUser
                {
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    UserName = signUpModel.Email,
                    Email = signUpModel.Email,
                    TwoFactorEnabled = true,
                };


                if (await _roleManage.RoleExistsAsync(role))
                {
                    IdentityResult result =  await _userManage.CreateAsync(user, signUpModel.Password); 
                    if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "User failed to create"
                        });
                        //List<IdentityError> errorList = result.Errors.ToList();
                        //string errors = "";

                        //foreach (var error in errorList)
                        //{
                        //    errors = errors + error.Description.ToString();
                        //}

                        //return Content(errors);
                    }

                    await _userManage.AddToRoleAsync(user, role);
                    var token = await _userManage.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                    var message = new User.Management.Service.Models.Message(new string[] { user.Email }, 
                        "Confirmation email link",
                        confirmationLink!);
                    _emailService.SendEmail(message);


                    return StatusCode(StatusCodes.Status200OK, new Response
                    {
                        Status = "Success",
                        Message = $"User created successfully and Send email to {user.Email}"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role don't exist" });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status416RangeNotSatisfiable, ex);
            }
                           
        }

        [HttpGet("ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail (string token, string email)
        {
            var user = await _userManage.FindByEmailAsync(email);
            if(user != null)
            {
                var result = await _userManage.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Email verified successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User don't exist" });

        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var user = await _userManage.FindByEmailAsync(signInModel.Email);
            if (user.TwoFactorEnabled)
            {
                await _loginManage.SignOutAsync();
                await _loginManage.PasswordSignInAsync(user, signInModel.Password, false, true);
                var token = await _userManage.GenerateTwoFactorTokenAsync(user, "Email");
                var message = new User.Management.Service.Models.Message(new string[] { user.Email! },
                    "OTP Confirmation",
                    token);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "We have sent a OTP to your email" });
            }
            if (user != null && await _userManage.CheckPasswordAsync(user, signInModel.Password))
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManage.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, role));
                }

              
                var jwtToken = GetToken(authClaim);
                return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                            expiration = jwtToken.ValidTo,
                        }
                    );
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Login-2FA")]

        public async Task<IActionResult> LoginWithOtp(string code, string email)
        {
            var signIn = await _loginManage.TwoFactorSignInAsync("Email", code, false, false);
            var user = await _userManage.FindByEmailAsync(email);
            if (signIn.Succeeded)
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManage.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = GetToken(authClaim);
                return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                            expiration = jwtToken.ValidTo,
                        }
                    );
            }

            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Invalid code" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]

        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManage.FindByEmailAsync(email);

            if (user != null)
            {
                var token  = await _userManage.GeneratePasswordResetTokenAsync(user);
                var forgotPassLink = Url.Action(nameof(ResetPassword), "Authentication", new {token, email = user.Email}, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password Link", forgotPassLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "We have sent a link for reset password to your email. Please open the link and click it for reset password!" });
            }
            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User isn't exist" });
        }

        [HttpGet]
        [Route("reset-password")]

        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };

            return Ok(new
            {
                model,
              
            });
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("reset-password")]

        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManage.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetResult = await _userManage.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

                if (!resetResult.Succeeded)
                {
                    foreach(var err in resetResult.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);

                    }
                    return Ok(ModelState);
                }

                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Password has been changed" });
            }

            return NotFound();

        }

        private JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires:DateTime.Now.AddHours(3),
                claims: authClaim,
                signingCredentials:new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));
            return token;
        }
        
    }
}
