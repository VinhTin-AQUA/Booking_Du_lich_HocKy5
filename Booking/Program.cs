using Booking.Configs;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Booking.Models.MailService;
using Booking.Repositories;
using Booking.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//sql server
builder.Services.AddDbContext<BookingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingConnectionString"));
});

// repositories service
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<IAuthenRepository, AuthenRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBusinessPartnerRepository, BusinessPartnerRespository>();
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackagePriceRepository, PackagePriceRepository>();
builder.Services.AddScoped<IBookTourRepository, BookTourRepository>();
//builder.Services.AddScoped<ITouristAttraction, TouristAttractionRepository>();
builder.Services.AddScoped<ITourCategoryRepository, TourCategoryRepository>();
//builder.Services.AddScoped<IVisitingRepository, VisitingRepository>();
builder.Services.AddScoped<ICityTourRepository, CityTourRepository>();

//identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<BookingContext>() // provide our context
    .AddDefaultTokenProviders() // create email for email confirmation
    .AddRoles<IdentityRole>() // be able to add roles
    .AddRoleManager<RoleManager<IdentityRole>>() // be able to make use of RoleManager
    .AddSignInManager<SignInManager<AppUser>>() // make use of sign in manager
    .AddUserManager<UserManager<AppUser>>(); // make use of user manager to create user

// configure user
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ " +
        "áãạàả" +
        "âấầậẫẩ" +
        "ăặắằẵẳ" +
        "éèẻẹẽ" +
        "êếểềệễ" +
        "íịỉĩì" +
        "ýỳỷỹỵ" +
        "úùủụũ" +
        "ưứủụữừ" +
        "óòỏọõ" +
        "ôốồổộỗ" +
        "ơớờởợỡ" +
        "đĐ";

    options.User.RequireUniqueEmail = true;  // Email là duy nhất
                                             // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;  // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

    // mặc định false, true => khi đăng ký => chuyển hướng đến trang RegisterConfirmation (xác thực email)
    options.SignIn.RequireConfirmedAccount = true;
});

//thiet lap cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


// mail service
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
builder.Services.AddSingleton(emailConfig);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
builder.Services.AddScoped<IEmailSender, EmailSender>();


// getconfig
var appConfigs = builder.Configuration
    .GetSection("Configs")
    .Get<AppConfigs>();
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
builder.Services.AddSingleton(appConfigs);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "ManageProfile",
    areaName: "ManageProfile",
    pattern: "{controller=Profile}/{action=Index}/{id?}");

#region Authentication

app.MapAreaControllerRoute(
    name: "Authentication",
    areaName: "Authentication",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Authentication",
    areaName: "Authentication",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

#endregion

#region admin route
app.MapAreaControllerRoute(
    name: "UserManager",
    areaName: "Admin",
    pattern: "{controller=UserManager}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "CityManager",
    areaName: "Admin",
    pattern: "{controller=City}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AgentHotelManager",
    areaName: "Admin",
    pattern: "{controller=AgentHotelManager}/{action=Index}/{id?}");
#endregion

#region AgentTour
app.MapAreaControllerRoute(
    name: "AgentTour",
    areaName: "AgentTour",
    pattern: "{controller=Tours}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AgentTour",
    areaName: "AgentTour",
    pattern: "{controller=Package}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AgentTour",
    areaName: "AgentTour",
    pattern: "{controller=PackagePrice}/{action=Index}/{id?}");

#endregion

using var scope = app.Services.CreateScope();
try
{
    var contextSeedService = scope.ServiceProvider.GetService<SeedService>();
    await contextSeedService!.InitializeContextAsync();
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
#pragma warning disable CS8604 // Possible null reference argument.
    logger.LogError("Eror: " + ex.InnerException, ex.InnerException);
#pragma warning restore CS8604 // Possible null reference argument.
}

app.Run();
