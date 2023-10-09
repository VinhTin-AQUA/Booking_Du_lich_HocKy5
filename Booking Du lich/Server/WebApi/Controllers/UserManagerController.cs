using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UserManagerController(IUserManagerRepository userManagerRepository)
        {
            this.userManagerRepository = userManagerRepository;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            var users = await userManagerRepository.GetUser(currentPage, pageSize);

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
                    LockoutEnd = user.LockoutEnd });
            }

            int totalUser = userManagerRepository.TotalUsers();

            return Ok(new {users = userView, totalUser = totalUser });
        }

    }
}
