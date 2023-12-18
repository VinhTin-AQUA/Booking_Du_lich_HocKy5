using Booking.Areas.Authentication.Models.Authentication;
using Booking.Interfaces;
using Booking.Models;
using Booking.Models.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Route("authentication")] // localhos:4200/authentication/sign-up
    public class AuthenticationController : Controller
    {
        private readonly IAuthenRepository authenRepository;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;
        private readonly IUserManagerRepository userManagerRepository;

        public AuthenticationController(IAuthenRepository authenRepository, RoleManager<IdentityRole> role, IConfiguration configuration, IEmailSender emailSender, IUserManagerRepository userManagerRepository)
        {
            this.authenRepository = authenRepository;
            _roleManage = role;
            _configuration = configuration;
            this.emailSender = emailSender;
            this.userManagerRepository = userManagerRepository;
        }


        // trả về view
        [HttpGet]
        [Route("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        // gửi dữ liệu từ view tới
        [Route("sign-up")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            //Check User exist
            var userExist = await authenRepository.GetUserByEmail(signUpModel.Email);

            if (userExist != null)
            {
                ModelState.AddModelError("Email", "Email này đã đăng ký. Vui lòng chọn email khác.");
                return View();
            }

            // Add user in the database
            var user = new AppUser
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                UserName = signUpModel.Email,
                Email = signUpModel.Email,
                Address = signUpModel.Address,
                PhoneNumber = signUpModel.PhoneNumber,
            };

            var result = await authenRepository.SignUp(user, signUpModel.Password);

            if (result.Succeeded == false)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }

                return View();
            }
            await authenRepository.AddRoleToUser(user, "User");
            var token = await authenRepository.GenerateEmailConfirmationToken(user);

            if (await emailSender.SendEmailConfirmAsync(user, "confirm-email", token) == true)
            {
                ViewBag.ReturnUrl = -1;
                return View("login");
            }
            ModelState.AddModelError(string.Empty, "Có lỗi. Xin vui lòng thử lại.");
            return View();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            ViewBag.ReturnUrl = -1;
            if (token == null || email == null)
            {
                return View("SignUp");
            }

            var user = await authenRepository.GetUserByEmail(email);

            if (user.EmailConfirmed == true)
            {
                return View("Login");
            }

            if (user != null)
            {
                var result = await authenRepository.ConfirmEmail(user, token);
                if (result.Succeeded)
                {
                    return View("Login");
                }
            }
            return View("SignUp");
        }

        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Login(int? returnUrl = -1)
        {
            if (await authenRepository.GetUserSignedIn(User) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel, [FromQuery]int? returnUrl = null)
        {

            if (signInModel == null || string.IsNullOrEmpty(signInModel.Email) || string.IsNullOrEmpty(signInModel.Password))
            {
                ModelState.AddModelError("Email", "Sai tài khoản hoặc mật khẩu");
                return View();
            }

            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError("Email", "Sai tài khoản hoặc mật khẩu");
                return View();
            }

            var user = await authenRepository.GetUserByEmail(signInModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Sai tài khoản hoặc mật khẩu");
                return View();
            }

            if (user.EmailConfirmed == false)
            {
                return View("ResendConfirmationEmail");
            }

            var result = await authenRepository.Login(user, signInModel.Password, signInModel.RememberMe);

            // kiểm tra khóa tài khoản
            if (result.IsLockedOut == true)
            {
                return View("Lockout", user.LockoutEnd!.Value.ToLocalTime());
            }

            if (result.Succeeded == false)
            {
                ModelState.AddModelError("Email", "Sai tài khoản hoặc mật khẩu.");
                return View();
            }

			if (returnUrl != -1)
			{
				return RedirectToAction("TourDetail", "Home", new { tourId = returnUrl });
			}

			return RedirectToAction("Index", "Home");
        }

        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await authenRepository.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Route("resend-confirmation-email")]
        public IActionResult ResendConfirmationEmail()
        {
            return View();
        }

        [HttpPost]
        [Route("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([Bind("Email")] ConfirmEmailModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var user = await authenRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email không chính xác.");
                return View();
            }
            var token = await authenRepository.GenerateEmailConfirmationToken(user);
            bool hasSent = await emailSender.SendEmailConfirmAsync(user, "", token);

            if (hasSent == false)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi trong quá trình xác thực. Xin vui lòng thử lại.");
                return View();
            }
            var notice = new Notice
            {
                Title = "Kiểm tra email của bạn",
                Description = "Tiếp tục xac thực email của bạn để sử dụng dịch vụ của chúng tôi"
            };
            return RedirectToAction("notification", "notice", notice);
        }

        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View((object)"Email không chính xác");
            }

            var userExisted = await authenRepository.GetUserByEmail(email);
            if (userExisted == null)
            {
                return View((object)"Email không chính xác");
            }
            var token = await authenRepository.GenerateResetPasswordToken(userExisted);
            if (await emailSender.SendEmailConfirmAsync(userExisted, "reset-password", token))
            {
                Notice notice = new Notice
                {
                    Title = "Kiểm tra email của bạn để lấy lại mật khẩu",
                    Description = "Xin cảm ơn đã sử dụng dịch vụ của chúng tôi"
                };
                return RedirectToAction("Notification", "notice", notice);
            }
            return View((object)"Xin vui lòng thử lại.");
        }

        [HttpGet]
        [Route("reset-password")]
        public IActionResult ResetPassword([FromQuery] string token, [FromQuery] string email)
        {
            ViewBag.token = token;
            ViewBag.email = email;
            return View();
        }

        [Route("reset-password")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (model == null)
            {
                Notice notice = new Notice
                {
                    Title = "Có lỗi xảy ra. XIn vui lòng thử lại",
                    Description = ""
                };
                return RedirectToAction("Notification", "Notice", notice);
            }

            if (ModelState.IsValid == false)
            {
                return View();
            }

            var userExisted = await authenRepository.GetUserByEmail(model.Email);

            if (userExisted == null)
            {
                Notice notice = new Notice
                {
                    Title = "Có lỗi xảy ra. XIn vui lòng thử lại",
                    Description = ""
                };
                return RedirectToAction("Notification", "Notice", notice);
            }

            // 
            var result = await authenRepository.ResetPassword(userExisted, model.Token, model.NewPassword);

            if (result.Succeeded == false)
            {
                Notice notice = new Notice
                {
                    Title = "Có lỗi xảy ra. XIn vui lòng thử lại",
                    Description = ""
                };
                return RedirectToAction("Notification", "Notice", notice);
            }

            return View("Login");
        }
    }
}
