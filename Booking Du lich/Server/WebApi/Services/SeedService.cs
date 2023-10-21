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
            await SeedAdmin();
            await SeedCities();
        }

        private async Task SeedRoles()
        {
            
            if (await roleManager.Roles.AnyAsync() == false)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.ADMIN_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.EMPLOYEE_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.AGENT_ROLE));
                await roleManager.CreateAsync(new IdentityRole(RoleSeed.USER_ROLE));
            }
        }

        private async Task SeedAdmin()
        {
            // seed user
            if (await userManager.Users.AnyAsync() == false)
            {
                // admin
                var admin = new ApplicationUser()
                {
                    UserName = AdminAccount.AdminEmail,
                    Email = AdminAccount.AdminEmail,
                    EmailConfirmed = true,
                    FirstName = AdminAccount.AdminFirstName,
                    LastName = AdminAccount.AdminLastName,
                    Address = AdminAccount.AdminAddress,
                };

                await userManager.CreateAsync(admin, AdminAccount.AdminPassword);

                await userManager.AddToRolesAsync(admin, new[] { RoleSeed.ADMIN_ROLE });
                await userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                });
            }
        }

        private async Task SeedCities()
        {
            if(await context.City.AnyAsync() == false)
            {
                List<City> cities = new List<City>()
                {
                    new City() { CityCode =  "HN-(29->33,40)", Name = "Hà Nội", PhotoPath = "/cities/ha_noi.jpg"},
                    new City() { CityCode =  "HCM-(50->59,41)", Name = "Hồ Chí Minh", PhotoPath = "/cities/ho_chi_minh.jpg"},
                    new City() { CityCode =  "HP-(15,16)", Name = "Hải Phòng", PhotoPath = "/cities/hai_phong.jpg"},
                    new City() { CityCode =  "DN-43", Name = "Đà Nẵng", PhotoPath = "/cities/da_nang.jpg"},
                    new City() { CityCode =  "HG-23", Name = "Hà Giang", PhotoPath = "/cities/ha_giang.jpg"},
                    new City() { CityCode =  "CB-11", Name = "Cao Bằng", PhotoPath = "/cities/cao_bang.jpg"},
                    new City() { CityCode =  "LC-25", Name = "Lai Châu", PhotoPath = "/cities/lai_chau.jpg"},
                    new City() { CityCode =  "LC-24", Name = "Lào Cai", PhotoPath = "/cities/lao_cai.jpg"},
                    new City() { CityCode =  "TQ-22", Name = "Tuyên Quang", PhotoPath = "/cities/tuyen_quang.jpg"},
                    new City() { CityCode =  "LS-12", Name = "Lạng Sơn", PhotoPath = "/cities/lang_son.jpg"},
                    new City() { CityCode =  "BK-97", Name = "Bắc Kạn", PhotoPath = "/cities/bac_kan.jpg"},
                    new City() { CityCode =  "TN-20", Name = "Thái Nguyên", PhotoPath = "/cities/thai_nguyen.jpg"},
                    new City() { CityCode =  "YB-21", Name = "Yên Bái", PhotoPath = "/cities/yen_bai.jpg"},
                    new City() { CityCode =  "SL-26", Name = "Sơn La", PhotoPath = "/cities/son_la.jpg"},
                    new City() { CityCode =  "PT-19", Name = "Phú Thọ", PhotoPath = "/cities/phu_tho.jpg"},
                    new City() { CityCode =  "VT-88", Name = "Vĩnh Phúc", PhotoPath = "/cities/vinh_phuc.jpg"},
                    new City() { CityCode =  "QN-14", Name = "Quảng Ninh", PhotoPath = "/cities/quang_ninh.jpg"},
                    new City() { CityCode =  "BG-(13,98)", Name = "Bắc Giang", PhotoPath = "/cities/bac_giang.jpg"},
                    new City() { CityCode =  "BN-99", Name = "Bắc Ninh", PhotoPath = "/cities/bac_ninh.jpg"},
                    new City() { CityCode =  "HD-34", Name = "Hải Dương", PhotoPath = "/cities/hai_duong.jpg"},
                    new City() { CityCode =  "H-89", Name = "Hưng Yên", PhotoPath = "/cities/hung_yen.jpg"},
                    new City() { CityCode =  "HB-28", Name = "Hòa Bình", PhotoPath = "/cities/hoa_binh.jpg"},
                    new City() { CityCode =  "HN-90", Name = "Hà Nam", PhotoPath = "/cities/ha_nam.jpg"},
                    new City() { CityCode =  "ND-18", Name = "Nam Định", PhotoPath = "/cities/nam_dinh.jpg"},
                    new City() { CityCode =  "TB-17", Name = "Thái Bình", PhotoPath = "/cities/thai_binh.jpg"},
                    new City() { CityCode =  "NB-35", Name = "Ninh Bình", PhotoPath = "/cities/ninh_binh.jpg"},
                    new City() { CityCode =  "TH-36", Name = "Thanh Hóa", PhotoPath = "/cities/thanh_hoa.jpg"},
                    new City() { CityCode =  "NA-37", Name = "Nghệ An", PhotoPath = "/cities/nghe_an.jpg"},
                    new City() { CityCode =  "HT-38", Name = "Hà Tĩnh", PhotoPath = "/cities/ha_tinh.jpg"},
                    new City() { CityCode =  "QB-73", Name = "Quảng Bình", PhotoPath = "/cities/quang_binh.jpg"},
                    new City() { CityCode =  "QT-74", Name = "Quảng Trị", PhotoPath = "/cities/quang_tri.jpg"},
                    new City() { CityCode =  "TTH-75", Name = "Thiên Huế", PhotoPath = "/cities/hue.jpg"},
                    new City() { CityCode =  "QN-92", Name = "Quảng Nam", PhotoPath = "/cities/quang_nam.jpg"},
                    new City() { CityCode =  "QN-76", Name = "Quảng Ngãi", PhotoPath = "/cities/quang_ngai.jpg"},
                    new City() { CityCode =  "KT-82", Name = "Kon Tum", PhotoPath = "/cities/kon_tum.jpg"},
                    new City() { CityCode =  "BD-77", Name = "Bình Định", PhotoPath = "/cities/binh_dinh.jpg"},
                    new City() { CityCode =  "GL-81", Name = "Gia Lai", PhotoPath = "/cities/gia_lai.jpg"},
                    new City() { CityCode =  "PY-78", Name = "Phú Yên", PhotoPath = "/cities/phu_yen.jpg"},
                    new City() { CityCode =  "DL-47", Name = "Đắk Lắk", PhotoPath = "/cities/dak_lak.jpg"},
                    new City() { CityCode =  "KH-79", Name = "Khánh Hòa", PhotoPath = "/cities/khanh_hoa.jpg"},
                    new City() { CityCode =  "LD-49", Name = "Lâm Đồng", PhotoPath = "/cities/lam_dong.jpg"},
                    new City() { CityCode =  "BP-93", Name = "Bình Phước", PhotoPath = "/cities/binh_phuoc.jpg"},
                    new City() { CityCode =  "BD-61", Name = "Bình Dương", PhotoPath = "/cities/binh_duong.jpg"},
                    new City() { CityCode =  "NT-85", Name = "Ninh Thuận", PhotoPath = "/cities/ninh_thuan.jpg"},
                    new City() { CityCode =  "TN-70", Name = "Tây Ninh", PhotoPath = "/cities/tay_ninh.jpg"},
                    new City() { CityCode =  "BT-86", Name = "Bình Thuận", PhotoPath = "/cities/binh_thuan.jpg"},
                    new City() { CityCode =  "DN-(39,60)", Name = "Đồng Nai", PhotoPath = "/cities/dong_nai.jpg"},
                    new City() { CityCode =  "LA-62", Name = "Long An", PhotoPath = "/cities/long_an.jpg"},
                    new City() { CityCode =  "DT-66", Name = "Đồng Tháp", PhotoPath = "/cities/dong_thap.jpg"},
                    new City() { CityCode =  "AG-67", Name = "An Giang", PhotoPath = "/cities/an_giang.jpg"},
                    new City() { CityCode =  "VT-72", Name = "Vũng Tàu", PhotoPath = "/cities/vung_tau.jpg"},
                    new City() { CityCode =  "TG-63", Name = "Tiền Giang", PhotoPath = "/cities/tien_giang.jpg"},
                    new City() { CityCode =  "KG-68", Name = "Kiên Giang", PhotoPath = "/cities/kien_giang.jpg"},
                    new City() { CityCode =  "CT-65", Name = "Cần Thơ", PhotoPath = "/cities/can_tho.jpg"},
                    new City() { CityCode =  "BT-71", Name = "Bến Tre", PhotoPath = "/cities/ben_tre.jpg"},
                    new City() { CityCode =  "VL-64", Name = "Vĩnh Long", PhotoPath = "/cities/vinh_long.jpg"},
                    new City() { CityCode =  "TV-84", Name = "Trà Vinh", PhotoPath = "/cities/tra_vinh.jpg"},
                    new City() { CityCode =  "ST-83", Name = "Sóc Trăng", PhotoPath = "/cities/soc_trang.jpg"},
                    new City() { CityCode =  "BL-94", Name = "Bạc Liêu", PhotoPath = "/cities/bac_lieu.jpg"},
                    new City() { CityCode =  "CM-69", Name = "Cà Mau", PhotoPath = "/cities/ca_mau.jpg"},
                    new City() { CityCode =  "DB-27", Name = "Điện Biên", PhotoPath = "/cities/dien_bien.jpg"},
                    new City() { CityCode =  "DN-48", Name = "Đắk Nông", PhotoPath = "/cities/dak_nong.jpg"},
                    new City() { CityCode =  "HG-95", Name = "Hậu Giang", PhotoPath = "/cities/hau_giang.jpg"},
                };
                context.City.AddRange(cities);
                await context.SaveChangesAsync();
            }
        }
    }
}
