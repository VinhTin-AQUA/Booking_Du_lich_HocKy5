using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Security;
using System.Text;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.MailService;
using WebApi.Repositories;
using WebApi.Services;
using WebApi1.Data;
using WebApi1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
});

// repositories service
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<IAuthenRepository, AuthenRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<IRoomPriceRepository, RoomPriceRepository>();
builder.Services.AddScoped<IBookRoomRepository, BookRoomRepository>();

builder.Services.AddScoped<ITourTypeRepository, TourTypeRepository>();

builder.Services.AddScoped<IBusinessPartnerRepository, BusinessPartnerRespository>();


builder.Services.AddScoped<ITourRepository, TourRepository>();


builder.Services.AddScoped<IHasServiceRepository, HasServiceRepository>();

builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackagePriceRepository, PackagePriceRepository>();
builder.Services.AddScoped<IBookTourRepository, BookTourRepository>();

// JWT
builder.Services.AddScoped<JWTService>();

// identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // validate the issuer (who ever is issuing the JWT)
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true, // validate token based on the key we have provided in appsetting.json

        // don't validate audience (angular side)
        ValidateAudience = false,

        //ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value,
        // the issuer which in here is the api project url
        ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value,

        // the issuer signin key based on JWT:Key
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value!))
    };
});

// Add config for user
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan= TimeSpan.FromHours(10));

// enable cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// mail service
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

// định dạng lỗi gửi đến client
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actionContext) =>
    {
        var errors = actionContext.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var toReturn = new
        {
            Errors = errors
        };

        return new BadRequestObjectResult(toReturn);
    };
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(option =>
option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// enable cors
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
try
{
    var contextSeedService = scope.ServiceProvider.GetService<SeedService>();
    await contextSeedService!.InitializeContextAsync();
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError("Eror: " + ex.InnerException, ex.InnerException);
}

app.Run();