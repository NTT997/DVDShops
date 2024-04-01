using DVDShops.Middlewares;
using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.Artists;
using DVDShops.Services.Genres;
using DVDShops.Services.MailService;
using DVDShops.Services.Producers;
using DVDShops.Services.Songs;
using DVDShops.Services.Suppliers;
using DVDShops.Services.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//configure connection to database
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DvdshopContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

//add scoped service
builder.Services.AddScoped<IUserService, UserService>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<IArtistService, ArtistService>()
                .AddScoped<ISongService, SongSerivce>()
                .AddScoped<IAlbumService, AlbumService>()
                .AddScoped<IGenreService, GenreService>()
                .AddScoped<IProducerService, ProducerService>()
                .AddScoped<ISupplierService, SupplierService>();


builder.Services.AddSession();

var app = builder.Build();
app.UseSession();

//only comment this line to test in admin
//app.UseMiddleware<LoginAuthMiddleware>();

app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "{area:exists}/{controller}/{action}");
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");
app.Run();

