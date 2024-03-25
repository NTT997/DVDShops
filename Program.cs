using DVDShops.Middlewares;
using DVDShops.Models;
using DVDShops.Services.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//configure connection to database
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DvdshopContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

//add scoped service
builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddSession();

var app = builder.Build();
app.UseSession();
app.UseMiddleware<LoginAuthMiddleware>();

app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "{area:exists}/{controller}/{action}");
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");
app.Run();

