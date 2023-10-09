using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.UserManager
{
    public class UserView
    {
        public string Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set;}

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
