using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.DTOs.Authentication;
using WebApi.DTOs.UserManager;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManagerRepository userManagerRepository;
        private readonly IAuthenRepository authenRepository;

        public UserManagerController(IUserManagerRepository userManagerRepository, IAuthenRepository authenRepository)
        {
            this.userManagerRepository = userManagerRepository;
            this.authenRepository = authenRepository;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers([FromQuery] int currentPage, [FromQuery] int pageSize, string? searchString)
        {
            var users = await userManagerRepository.GetUsers(currentPage, pageSize, searchString);

            var userView = new List<UserView>();

            foreach (var user in users)
            {
                userView.Add(new UserView
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    LockoutEnd = user.LockoutEnd
                });
            }

            int totalUser = userView.Count();

            return Ok(new { users = userView, totalUser = totalUser });
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById([FromQuery] string userId)
        {
            var userModel = await userManagerRepository.GetUserById(userId);

            var user = new UserView
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                EmailConfirmed = userModel.EmailConfirmed,
                PhoneNumber = userModel.PhoneNumber,
                LockoutEnd = userModel.LockoutEnd,
                Address = userModel.Address
            };

            return Ok(new { user });
        }

        [HttpPut("lock-user")]
        public async Task<IActionResult> LockUser([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new JsonResult(new {tile = "Error", message = "Something error when lock user"}));
            }

            var user = await authenRepository.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest(new JsonResult(new { tile = "Error", message = "User was not found" }));
            }

            var result = await userManagerRepository.LockUser(user);
            if (result.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { tile = "Error", message = "Something error when lock user" }));
            }
            
            return Ok(new JsonResult(new { tile = "Success", lockoutEnd = user.LockoutEnd }));
        }

        [HttpPut("un-lock-user")]
        public async Task<IActionResult> UnlockUser([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new JsonResult(new { tile = "Error", message = "Something error when un-lock user" }));
            }

            var user = await authenRepository.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest(new JsonResult(new { tile = "Error", message = "User was not found" }));
            }

            var result = await userManagerRepository.UnlockUser(user);
            if (result.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { tile = "Error", message = "Something error when un-lock user" }));
            }

            return Ok(new JsonResult(new { tile = "Success", message = "Unlock user successfully" }));
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when delete user" }));
            }

            var user = await authenRepository.GetUserByEmail(email);
            if(user == null)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Use was not found" }));
            }

            var result = await userManagerRepository.DeleteUser(user);
            if(result.Succeeded == false)
            {
                return BadRequest(new JsonResult(new { title = "Error", message = "Something error when delete user" }));
            }
            return Ok(new JsonResult(new { title = "Success", message = "Delete user successfully" }));
        }
    }
}
