using DVDShops.Middlewares;
using DVDShops.Models;
using DVDShops.Services.Albums;
using DVDShops.Services.AlbumsSongs;
using DVDShops.Services.Artists;
using DVDShops.Services.ArtistsGenres;
using DVDShops.Services.Games;
using DVDShops.Services.GamesGenres;
using DVDShops.Services.Genres;
using DVDShops.Services.MailService;
using DVDShops.Services.Movies;
using DVDShops.Services.Moviesgenres;
using DVDShops.Services.Producers;
using DVDShops.Services.Products;
using DVDShops.Services.Promotions;
using DVDShops.Services.Songs;
using DVDShops.Services.SongsGenres;
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
                .AddScoped<IProducerService, ProducerService>()
                .AddScoped<IProductService, Productservice>()
                .AddScoped<IPromotionService, PromotionService>()
                .AddScoped<ISupplierService, SupplierService>()
                .AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IArtistService, ArtistService>()
                .AddScoped<IArtistGenreService, ArtistGenreService>();

builder.Services.AddScoped<ISongService, SongSerivce>()
                .AddScoped<ISongGenreService, SongGenreService>();

builder.Services.AddScoped<IAlbumService, AlbumService>()
                .AddScoped<IAlbumsSongsService, AlbumsSongsService>();

builder.Services.AddScoped<IGameService, GameService>()
                .AddScoped<IGameGenreService, GameGenreService>();

builder.Services.AddScoped<IMovieService, MovieService>()
                .AddScoped<IMovieGenreService, MovieGenreService>();

builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();


app.UseRouting();
app.UseSession();

//only comment this line to test in admin
app.UseMiddleware<LoginAuthMiddleware>();

app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "{area:exists}/{controller}/{action}");
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");

app.Run();

