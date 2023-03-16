using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Main.Data;
using server.Main.Service;

var allowSpecificOrigin = "_allowedOrigin";
var builder = WebApplication.CreateBuilder(args);

// ==============================
// Add services to the container.
// ==============================

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigin, policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

// Add Database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IAzureStorage, AzureStorage>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


// Build
var app = builder.Build();


// ==============================
// Add Middelware.
// ==============================
app.UseRouting();
app.UseCors(allowSpecificOrigin);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
