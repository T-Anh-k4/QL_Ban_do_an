using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using QLBH.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database context
var connectionString = builder.Configuration.GetConnectionString("QlbandoanContext");
builder.Services.AddDbContext<QlbandoanContext>(options =>
    options.UseSqlServer(connectionString));

// Add scoped services (repositories)
builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();

// Add session services
builder.Services.AddSession();

// Add Authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Access/Login";
        options.AccessDeniedPath = "/Access/Denied";
    });

// Build the application
var app = builder.Build();


// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Enable session middleware
app.UseSession();

// Configure the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");


app.Run();
