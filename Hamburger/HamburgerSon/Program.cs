using HamburgerWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<HamburgerContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddIdentity<AppUser, IdentityRole>(pas =>
{
    pas.Password.RequireUppercase = false;
    pas.Password.RequiredLength = 8;
    pas.Password.RequireNonAlphanumeric = false;
}

).AddEntityFrameworkStores<HamburgerContext>();
builder.Services.AddScoped<UserManager<AppUser>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "MyCookie";
    opt.LoginPath = "/User/SignIn";


    opt.SlidingExpiration = true;
    opt.ExpireTimeSpan = TimeSpan.FromHours(12);
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    options.AccessDeniedPath = "/Login/Index"; // Yetkisi olmayan kullanýcýnýn gideceði yer
});

builder.Services.AddAuthorization();
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
