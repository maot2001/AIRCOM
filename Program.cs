using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy => policy.RequireClaim("UserType", "Cliente"));
    options.AddPolicy("Security", policy => policy.RequireClaim("UserType", "Seguridad"));
    options.AddPolicy("Mechanic", policy => policy.RequireClaim("UserType", "Mecanico"));
    options.AddPolicy("Direction", policy => policy.RequireClaim("UserType", "Direccion"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("UserType", "Administrador"));
});

    
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

// Service Layer
builder.Services.AddScoped<AirportService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<InstallationService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<On_siteService>();
builder.Services.AddScoped<RepairInstallationService>();
builder.Services.AddScoped<RepairService>();
builder.Services.AddScoped<RepairShipService>();
builder.Services.AddScoped<ServiceInstallationService>();
builder.Services.AddScoped<ServicesService>();
builder.Services.AddScoped<ShipsService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

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

app.Run();
