using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedEmployee
    {
        public static async Task<string[]> SeedEmployeeAsync(UserManager<ApplicationUser> userManager)
        {
            string[] employeeIds = new string[] { "", "", "", "", "" };

            string fakePassword = "default123";
            var passwordHasher = new PasswordHasher<IdentityUser>();
            string hashedPassword = passwordHasher.HashPassword(null, fakePassword);

            Faker<ApplicationUser> fk = new Faker<ApplicationUser>();

            fk.RuleFor(u => u.FirstName, f => f.Name.FirstName());
            fk.RuleFor(u => u.LastName, f => f.Name.LastName());
            fk.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
            fk.RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper());
            fk.RuleFor(u => u.EmailConfirmed, f => true);
            fk.RuleFor(u => u.UserName, (f, u) => u.Email);
            fk.RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper());
            fk.RuleFor(u => u.PasswordHash, f => hashedPassword);
            fk.RuleFor(u => u.Address, f => f.Address.FullAddress());
            fk.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));

            for (int i = 1; i <= 5; i++)
            {
                var employee = fk.Generate();

                await userManager.CreateAsync(employee);

                await userManager.AddToRolesAsync(employee, new[] { SeedRole.EMPLOYEE_ROLE });
                await userManager.AddClaimsAsync(employee, new Claim[]
                {
                    new Claim(ClaimTypes.Email, employee.Email),
                });
                employeeIds[i - 1] = employee.Id;
            }
            return employeeIds;
        }
    }
}
