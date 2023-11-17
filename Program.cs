using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authentication;
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
    options.AddPolicy("UserClient", policy => policy.RequireClaim("UserType", "Client"));
    options.AddPolicy("UserSecurity", policy => policy.RequireClaim("UserType", "Security"));
    options.AddPolicy("UserMechanic", policy => policy.RequireClaim("UserType", "Mechanic"));
    options.AddPolicy("UserDirection", policy => policy.RequireClaim("UserType", "Direction"));
    options.AddPolicy("UserAdmin", policy => policy.RequireClaim("UserType", "Admin")); 
}); 

builder.Services.AddDbContext<DBContext>(options =>
{
    var defaults = builder.Configuration.GetConnectionString("DBConnection");
    if (!options.IsConfigured)
        options.UseMySql(defaults, ServerVersion.AutoDetect(defaults));
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

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DBContext>();
    
    dataContext.Database.Migrate();
}

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
