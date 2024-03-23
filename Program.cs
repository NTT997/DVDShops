using DVDShops.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//configure connection to database
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DvdshopContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

//add scoped service



var app = builder.Build();
app.UseSession();
app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "{area:exists}/{controller}/{action}");
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");
app.Run();

