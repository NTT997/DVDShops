using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DVDShops.Models;

public partial class DvdshopContext : DbContext
{
    public DvdshopContext()
    {
    }

    public DvdshopContext(DbContextOptions<DvdshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminsPermission> AdminsPermissions { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumsGenre> AlbumsGenres { get; set; }

    public virtual DbSet<AlbumsSong> AlbumsSongs { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<ArtistsGenre> ArtistsGenres { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamesGenre> GamesGenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MoviesGenre> MoviesGenres { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongsGenre> SongsGenres { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminsPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminsPe__3213E83FBDAEEC9A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.AdminsPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_permissions");

            entity.HasOne(d => d.Users).WithMany(p => p.AdminsPermissions)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_admins");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__B0E1DDB219ECD824");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.AlbumCover)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("album_cover");
            entity.Property(e => e.AlbumIntroduction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("album_introduction");
            entity.Property(e => e.AlbumName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("album_name");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_artist");

            entity.HasOne(d => d.Category).WithMany(p => p.Albums)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_cate");

            entity.HasOne(d => d.Producer).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_producer");

            entity.HasOne(d => d.Review).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("fk_album_review");
        });

        modelBuilder.Entity<AlbumsGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlbumsGe__3213E83F67B61373");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumsGenres)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_AlbumsGenres_album");

            entity.HasOne(d => d.Genre).WithMany(p => p.AlbumsGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_AlbumsGenres_song");
        });

        modelBuilder.Entity<AlbumsSong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlbumsSo__3213E83F85521797");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumsSongs)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_AlbumsSongs_album");

            entity.HasOne(d => d.Song).WithMany(p => p.AlbumsSongs)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_AlbumsSongs_song");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__6CD040011E53225B");

            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.ArtistBiography)
                .HasColumnType("text")
                .HasColumnName("artist_biography");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("artist_name");
            entity.Property(e => e.ArtistPhoto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("artist_photo");
        });

        modelBuilder.Entity<ArtistsGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ArtistsG__3213E83FEB89DF7F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.ArtistsGenres)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Artists_Genres_artist");

            entity.HasOne(d => d.Genre).WithMany(p => p.ArtistsGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Artists_Genres_genre");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Carts__2EF52A27605A545C");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_product");

            entity.HasOne(d => d.Users).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_user");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B497D6B821");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8CD21F4295");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.FeedbackContent)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("feedback_content");
            entity.Property(e => e.FeedbackDate).HasColumnName("feedback_date");
            entity.Property(e => e.FeedbackReply)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("feedback_reply");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_member");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__FFE11FCF4AB961A2");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DownloadLink)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("download_link");
            entity.Property(e => e.GameCover)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("game_cover");
            entity.Property(e => e.GameDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("game_description");
            entity.Property(e => e.GameTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("game_title");
            entity.Property(e => e.GameTrailer)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("game_trailer");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Games)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_category");

            entity.HasOne(d => d.Producer).WithMany(p => p.Games)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_producer");
        });

        modelBuilder.Entity<GamesGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GamesGen__3213E83FE687BF8B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Game).WithMany(p => p.GamesGenres)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("fk_gg_game");

            entity.HasOne(d => d.Genre).WithMany(p => p.GamesGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("fk_gg_genre");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__18428D42EE4CEB28");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("genre_description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD499E546023");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_invoice_order");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__InvoiceD__38E9A224FBE9FC07");

            entity.ToTable("InvoiceDetail");

            entity.Property(e => e.DetailId).HasColumnName("detail_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PriceAfterPromotion).HasColumnName("price_after_promotion");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("fk_detail_invoice");

            entity.HasOne(d => d.Order).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_detail_order");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_detail_product");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__83CDF7494B05C890");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DownloadLink)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("download_link");
            entity.Property(e => e.MovieCover)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("movie_cover");
            entity.Property(e => e.MovieDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("movie_description");
            entity.Property(e => e.MovieTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_title");
            entity.Property(e => e.MovieTrailer)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("movie_trailer");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_category");

            entity.HasOne(d => d.Producer).WithMany(p => p.Movies)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_producer");
        });

        modelBuilder.Entity<MoviesGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MoviesGe__3213E83FC2D76F66");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.MoviesGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("fk_mg_genre");

            entity.HasOne(d => d.Movie).WithMany(p => p.MoviesGenres)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("fk_mg_game");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD8D639FDB0");

            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.NewsContent)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("news_content");
            entity.Property(e => e.NewsImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("news_image");
            entity.Property(e => e.NewsSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("news_source");
            entity.Property(e => e.NewsTitle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("news_title");
            entity.Property(e => e.PublishDate).HasColumnName("publish_date");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229D0C0E05B");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ship_address");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_user");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__E5331AFABBBE3000");

            entity.ToTable("Permission");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("permission_type");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.ProducerId).HasName("PK__Producer__EA7F30C84BC3FE35");

            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.DeleteStatus).HasColumnName("delete_status");
            entity.Property(e => e.ProducerIntroduction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("producer_introduction");
            entity.Property(e => e.ProducerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("producer_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__47027DF552B168AA");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ProductQuantity).HasColumnName("product_quantity");
            entity.Property(e => e.ProductRate).HasColumnName("product_rate");
            entity.Property(e => e.ProductTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_title");
            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.SoldUnit).HasColumnName("sold_unit");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Album).WithMany(p => p.Products)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_product_album");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Products)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_promotion");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_supplier");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556B950DA316");

            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.PromotionBanner)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("promotion_banner");
            entity.Property(e => e.PromotionExpireDate).HasColumnName("promotion_expire_date");
            entity.Property(e => e.PromotionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("promotion_name");
            entity.Property(e => e.PromotionPercent).HasColumnName("promotion_percent");
            entity.Property(e => e.PromotionStartDate).HasColumnName("promotion_start_date");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__D35B278B4520389F");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating1).HasColumnName("rating");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_rating_product");

            entity.HasOne(d => d.Users).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("fk_rating_user");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D901C1611F0");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.ReviewContent)
                .HasColumnType("text")
                .HasColumnName("review_content");
            entity.Property(e => e.ReviewTitle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("review_title");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_review_user");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__A535AE1C799C605A");

            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DownloadLink)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("download_link");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.SongCover)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("song_cover");
            entity.Property(e => e.SongIntroduction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("song_introduction");
            entity.Property(e => e.SongName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("song_name");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_artist");

            entity.HasOne(d => d.Category).WithMany(p => p.Songs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_category");

            entity.HasOne(d => d.Producer).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_producer");
        });

        modelBuilder.Entity<SongsGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongsGen__3213E83F636CC910");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.SongsGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_SongsGenres_genre");

            entity.HasOne(d => d.Song).WithMany(p => p.SongsGenres)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_SongsGenres_song");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E880F2BD71");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.DeleteStatus).HasColumnName("delete_status");
            entity.Property(e => e.SupplierAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("supplier_address");
            entity.Property(e => e.SupplierEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("supplier_email");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("supplier_name");
            entity.Property(e => e.SupplierPhone).HasColumnName("supplier_phone");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__Users__EAA7D14B0C61A461");

            entity.HasIndex(e => e.UsersEmail, "UQ__Users__D156B4FE8F15A401").IsUnique();

            entity.Property(e => e.UsersId).HasColumnName("users_id");
            entity.Property(e => e.DeleteStatus).HasColumnName("delete_status");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.IsCancel).HasColumnName("is_cancel");
            entity.Property(e => e.IsCustomer).HasColumnName("is_customer");
            entity.Property(e => e.UsersActivated).HasColumnName("users_activated");
            entity.Property(e => e.UsersAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("users_address");
            entity.Property(e => e.UsersDateOfBirth).HasColumnName("users_date_of_birth");
            entity.Property(e => e.UsersEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("users_email");
            entity.Property(e => e.UsersPassword)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("users_password");
            entity.Property(e => e.UsersPhone).HasColumnName("users_phone");
            entity.Property(e => e.UsersProfileName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("users_profile_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
