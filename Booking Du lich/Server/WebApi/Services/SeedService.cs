using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Models;
using WebApi.Seeds;
using WebApi1.Data;

namespace WebApi.Services
{
    public class SeedService
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public async Task InitializeContextAsync()
        {
            // kiểm tra có migration nào ở trạng thái pending không
            if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                // tiến hành cập nhật migration
                await context.Database.MigrateAsync();
            }
            await SeedRoles();

            if (await userManager.Users.AnyAsync() == false)
            {
                await SeedAdmin();
                await SeedAgent();
                await SeedEmployee();
                await SeedUserAsync();
            }
            await SeedCities();
        }

        private async Task SeedRoles()
        {
            if (await roleManager.Roles.AnyAsync() == false)
            {
                await roleManager.CreateAsync(new IdentityRole(SeedRole.ADMIN_ROLE));
                await roleManager.CreateAsync(new IdentityRole(SeedRole.EMPLOYEE_ROLE));
                await roleManager.CreateAsync(new IdentityRole(SeedRole.AGENTHOTEL_ROLE));
                await roleManager.CreateAsync(new IdentityRole(SeedRole.AGENTTOUR_ROLE));
                await roleManager.CreateAsync(new IdentityRole(SeedRole.USER_ROLE));
            }
        }

        private async Task SeedAdmin()
        {
            var admin = new ApplicationUser()
            {
                UserName = Seeds.SeedAdmin.Email,
                Email = Seeds.SeedAdmin.Email,
                EmailConfirmed = true,
                FirstName = Seeds.SeedAdmin.FirstName,
                LastName = Seeds.SeedAdmin.LastName,
                Address = Seeds.SeedAdmin.Address,
            };

            await userManager.CreateAsync(admin, Seeds.SeedAdmin.Password);

            await userManager.AddToRolesAsync(admin, new[] { SeedRole.ADMIN_ROLE });
            await userManager.AddClaimsAsync(admin, new Claim[]
            {
                    new Claim(ClaimTypes.Email, admin.Email),
            });
        }

        private async Task SeedAgent()
        {
            var agentHotel = new ApplicationUser()
            {
                UserName = SeedAgentHotel.Email,
                Email = SeedAgentHotel.Email,
                EmailConfirmed = true,
                FirstName = SeedAgentHotel.FirstName,
                LastName = SeedAgentHotel.LastName,
                Address = SeedAgentHotel.Address,
            };

            await userManager.CreateAsync(agentHotel, SeedAgentHotel.Password);

            await userManager.AddToRolesAsync(agentHotel, new[] { SeedRole.AGENTHOTEL_ROLE });
            await userManager.AddClaimsAsync(agentHotel, new Claim[]
            {
                    new Claim(ClaimTypes.Email, agentHotel.Email),
            });

            // agent tour
            var agentTour = new ApplicationUser()
            {
                UserName = SeedAgentTour.Email,
                Email = SeedAgentTour.Email,
                EmailConfirmed = true,
                FirstName = SeedAgentTour.FirstName,
                LastName = SeedAgentTour.LastName,
                Address = SeedAgentTour.Address,
            };

            await userManager.CreateAsync(agentTour, SeedAgentTour.Password);

            await userManager.AddToRolesAsync(agentTour, new[] { SeedRole.AGENTTOUR_ROLE });
            await userManager.AddClaimsAsync(agentTour, new Claim[]
            {
                    new Claim(ClaimTypes.Email, agentTour.Email),
            });
        }

        private async Task SeedEmployee()
        {
            var employee = new ApplicationUser()
            {
                UserName = Seeds.SeedEmployee.Email,
                Email = Seeds.SeedEmployee.Email,
                EmailConfirmed = true,
                FirstName = Seeds.SeedEmployee.FirstName,
                LastName = Seeds.SeedEmployee.LastName,
                Address = Seeds.SeedEmployee.Address,
            };

            await userManager.CreateAsync(employee, Seeds.SeedEmployee.Password);

            await userManager.AddToRolesAsync(employee, new[] { SeedRole.EMPLOYEE_ROLE });
            await userManager.AddClaimsAsync(employee, new Claim[]
            {
                    new Claim(ClaimTypes.Email, employee.Email),
            });
        }

        private async Task SeedUserAsync()
        {
            var user = new ApplicationUser()
            {
                UserName = SeedUser.Email,
                Email = SeedUser.Email,
                EmailConfirmed = true,
                FirstName = SeedUser.FirstName,
                LastName = SeedUser.LastName,
                Address = SeedUser.Address,
            };

            await userManager.CreateAsync(user, SeedUser.Password);

            await userManager.AddToRolesAsync(user, new[] { SeedRole.USER_ROLE });
            await userManager.AddClaimsAsync(user, new Claim[]
            {
                    new Claim(ClaimTypes.Email, user.Email),
            });
        }

        private async Task SeedCities()
        {
            if (await context.City.AnyAsync() == false)
            {
                List<City> cities = new List<City>()
                {
                    new City() {  Name = "Hà Nội", PhotoPath = "/cities/ha_noi.jpg"},
                    new City() {  Name = "Hồ Chí Minh", PhotoPath = "/cities/ho_chi_minh.jpg"},
                    new City() {  Name = "Hải Phòng", PhotoPath = "/cities/hai_phong.jpg"},
                    new City() {  Name = "Đà Nẵng", PhotoPath = "/cities/da_nang.jpg"},
                    new City() {  Name = "Hà Giang", PhotoPath = "/cities/ha_giang.jpg"},
                    new City() {  Name = "Cao Bằng", PhotoPath = "/cities/cao_bang.jpg"},
                    new City() {  Name = "Lai Châu", PhotoPath = "/cities/lai_chau.jpg"},
                    new City() {  Name = "Lào Cai", PhotoPath = "/cities/lao_cai.jpg"},
                    new City() {  Name = "Tuyên Quang", PhotoPath = "/cities/tuyen_quang.jpg"},
                    new City() {  Name = "Lạng Sơn", PhotoPath = "/cities/lang_son.jpg"},
                    new City() {  Name = "Bắc Kạn", PhotoPath = "/cities/bac_kan.jpg"},
                    new City() {  Name = "Thái Nguyên", PhotoPath = "/cities/thai_nguyen.jpg"},
                    new City() {  Name = "Yên Bái", PhotoPath = "/cities/yen_bai.jpg"},
                    new City() {  Name = "Sơn La", PhotoPath = "/cities/son_la.jpg"},
                    new City() {  Name = "Phú Thọ", PhotoPath = "/cities/phu_tho.jpg"},
                    new City() {  Name = "Vĩnh Phúc", PhotoPath = "/cities/vinh_phuc.jpg"},
                    new City() {  Name = "Quảng Ninh", PhotoPath = "/cities/quang_ninh.jpg"},
                    new City() {  Name = "Bắc Giang", PhotoPath = "/cities/bac_giang.jpg"},
                    new City() {  Name = "Bắc Ninh", PhotoPath = "/cities/bac_ninh.jpg"},
                    new City() {  Name = "Hải Dương", PhotoPath = "/cities/hai_duong.jpg"},
                    new City() {  Name = "Hưng Yên", PhotoPath = "/cities/hung_yen.jpg"},
                    new City() {  Name = "Hòa Bình", PhotoPath = "/cities/hoa_binh.jpg"},
                    new City() {  Name = "Hà Nam", PhotoPath = "/cities/ha_nam.jpg"},
                    new City() {  Name = "Nam Định", PhotoPath = "/cities/nam_dinh.jpg"},
                    new City() {  Name = "Thái Bình", PhotoPath = "/cities/thai_binh.jpg"},
                    new City() {  Name = "Ninh Bình", PhotoPath = "/cities/ninh_binh.jpg"},
                    new City() {  Name = "Thanh Hóa", PhotoPath = "/cities/thanh_hoa.jpg"},
                    new City() {  Name = "Nghệ An", PhotoPath = "/cities/nghe_an.jpg"},
                    new City() {  Name = "Hà Tĩnh", PhotoPath = "/cities/ha_tinh.jpg"},
                    new City() {  Name = "Quảng Bình", PhotoPath = "/cities/quang_binh.jpg"},
                    new City() {  Name = "Quảng Trị", PhotoPath = "/cities/quang_tri.jpg"},
                    new City() {  Name = "Thiên Huế", PhotoPath = "/cities/hue.jpg"},
                    new City() {  Name = "Quảng Nam", PhotoPath = "/cities/quang_nam.jpg"},
                    new City() {  Name = "Quảng Ngãi", PhotoPath = "/cities/quang_ngai.jpg"},
                    new City() {  Name = "Kon Tum", PhotoPath = "/cities/kon_tum.jpg"},
                    new City() {  Name = "Bình Định", PhotoPath = "/cities/binh_dinh.jpg"},
                    new City() {  Name = "Gia Lai", PhotoPath = "/cities/gia_lai.jpg"},
                    new City() {  Name = "Phú Yên", PhotoPath = "/cities/phu_yen.jpg"},
                    new City() {  Name = "Đắk Lắk", PhotoPath = "/cities/dak_lak.jpg"},
                    new City() {  Name = "Khánh Hòa", PhotoPath = "/cities/khanh_hoa.jpg"},
                    new City() {  Name = "Lâm Đồng", PhotoPath = "/cities/lam_dong.jpg"},
                    new City() {  Name = "Bình Phước", PhotoPath = "/cities/binh_phuoc.jpg"},
                    new City() {  Name = "Bình Dương", PhotoPath = "/cities/binh_duong.jpg"},
                    new City() {  Name = "Ninh Thuận", PhotoPath = "/cities/ninh_thuan.jpg"},
                    new City() {  Name = "Tây Ninh", PhotoPath = "/cities/tay_ninh.jpg"},
                    new City() {  Name = "Bình Thuận", PhotoPath = "/cities/binh_thuan.jpg"},
                    new City() {  Name = "Đồng Nai", PhotoPath = "/cities/dong_nai.jpg"},
                    new City() {  Name = "Long An", PhotoPath = "/cities/long_an.jpg"},
                    new City() {  Name = "Đồng Tháp", PhotoPath = "/cities/dong_thap.jpg"},
                    new City() {  Name = "An Giang", PhotoPath = "/cities/an_giang.jpg"},
                    new City() {  Name = "Vũng Tàu", PhotoPath = "/cities/vung_tau.jpg"},
                    new City() {  Name = "Tiền Giang", PhotoPath = "/cities/tien_giang.jpg"},
                    new City() {  Name = "Kiên Giang", PhotoPath = "/cities/kien_giang.jpg"},
                    new City() {  Name = "Cần Thơ", PhotoPath = "/cities/can_tho.jpg"},
                    new City() {  Name = "Bến Tre", PhotoPath = "/cities/ben_tre.jpg"},
                    new City() {  Name = "Vĩnh Long", PhotoPath = "/cities/vinh_long.jpg"},
                    new City() {  Name = "Trà Vinh", PhotoPath = "/cities/tra_vinh.jpg"},
                    new City() {  Name = "Sóc Trăng", PhotoPath = "/cities/soc_trang.jpg"},
                    new City() {  Name = "Bạc Liêu", PhotoPath = "/cities/bac_lieu.jpg"},
                    new City() {  Name = "Cà Mau", PhotoPath = "/cities/ca_mau.jpg"},
                    new City() {  Name = "Điện Biên", PhotoPath = "/cities/dien_bien.jpg"},
                    new City() {  Name = "Đắk Nông", PhotoPath = "/cities/dak_nong.jpg"},
                    new City() {  Name = "Hậu Giang", PhotoPath = "/cities/hau_giang.jpg"},
                };
                context.City.AddRange(cities);
                await context.SaveChangesAsync();
            }
        }
    }
}
