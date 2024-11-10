using BonsaiShop_API;
using BonsaiShop_API.Areas.Service;
using BonsaiShop_API.DALL.Repositories;
using BonsaiShop_API.DALL.RepositoriesImplement;
using BonsaiShop_API.MappingProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Register services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    // Áp dụng `SwaggerFileOperationFilter` để hỗ trợ upload file
    c.OperationFilter<SwaggerFileOperationFilter>();
});


// Use existing builder.Configuration to load settings
builder.Services.AddDbContext<BonsaiDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Bonsai")));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ITokenBlacklistRepository, RedisTokenBlacklistRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IGardenRepository, GardenRepository>();





// Register services mapping
builder.Services.AddAutoMapper(typeof(UserRegister_User));
builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddAutoMapper(typeof(GardenProfile));
builder.Services.AddAutoMapper(typeof(ImageProfile));
builder.Services.AddAutoMapper(typeof(PlantProfile));





// Phan quyen
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("GardenOwnerOnly", policy => policy.RequireRole("GARDEN"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("USER"));
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var tokenBlacklistRepository = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistRepository>();
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (await tokenBlacklistRepository.IsTokenBlacklistedAsync(token))
                {
                    context.Fail("This token has been blacklisted");
                }
            }
        };
    });


builder.Services.AddControllers();

var app = builder.Build();

// Middleware configuration
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Make sure to use authentication middleware
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
