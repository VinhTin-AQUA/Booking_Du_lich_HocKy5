using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Seeds
{
    public static class SeedCity
    {
        public static async Task SeedCitiesAsync(ApplicationDbContext context)
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
