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

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__B0E1DDB2D5AE7DF3");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.AlbumIntroduction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("album_introduction");
            entity.Property(e => e.AlbumPrice).HasColumnName("album_price");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.SoldUnit).HasColumnName("sold_unit");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_artist");

            entity.HasOne(d => d.Feedback).WithMany(p => p.Albums)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_feedback");

            entity.HasOne(d => d.Producer).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_producer");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__6CD040016B988E73");

            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("artist_name");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("bio");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.Artists)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_artist_genre");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Carts__2EF52A27F40EAE3C");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Carts)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_promotion");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B49BC02F9F");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8C34E91F9F");

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
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_product");

            entity.HasOne(d => d.Users).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_member");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__FFE11FCF1E2AADE0");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
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
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Games)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_category");

            entity.HasOne(d => d.Genre).WithMany(p => p.Games)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_genre");

            entity.HasOne(d => d.Producer).WithMany(p => p.Games)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_producer");

            entity.HasOne(d => d.Product).WithMany(p => p.Games)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_product");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__18428D42ACB3456F");

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
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__F58DFD4971B7433F");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ship_address");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_order");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__83CDF7491522F47D");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MovieIntro)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("movie_intro");
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
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_category");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_genre");

            entity.HasOne(d => d.Producer).WithMany(p => p.Movies)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_producer");

            entity.HasOne(d => d.Product).WithMany(p => p.Movies)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_product");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD815E416D4");

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
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229CBD64AC3");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_cart");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Orders)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_invoice");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_user");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.ProducerId).HasName("PK__Producer__EA7F30C84B102849");

            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
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
            entity.HasKey(e => e.ProductId).HasName("PK__Products__47027DF51E9EBD03");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Album).WithMany(p => p.Products)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_product_album");

            entity.HasOne(d => d.Feedback).WithMany(p => p.Products)
                .HasForeignKey(d => d.FeedbackId)
                .HasConstraintName("fk_product_feedback");

            entity.HasOne(d => d.Game).WithMany(p => p.Products)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("fk_product_game");

            entity.HasOne(d => d.Genre).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_genre");

            entity.HasOne(d => d.Producer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_producer");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Products)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_promotion");

            entity.HasOne(d => d.Song).WithMany(p => p.Products)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("fk_product_song");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_supplier");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556BAE3E99CC");

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

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__779B7C58D36D1C00");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.ReportContent)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("report_content");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("fk_report_user");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__A535AE1CD45D1ED7");

            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Intro)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("intro");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.SoldUnit).HasColumnName("sold_unit");
            entity.Property(e => e.SongName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("song_name");
            entity.Property(e => e.SongPrice).HasColumnName("song_price");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_song_album");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_artist");

            entity.HasOne(d => d.Category).WithMany(p => p.Songs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_category");

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_genre");

            entity.HasOne(d => d.Producer).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_producer");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E80F27AD54");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
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
            entity.HasKey(e => e.UsersId).HasName("PK__Users__EAA7D14BE5C1EDF8");

            entity.HasIndex(e => e.UsersEmail, "UQ__Users__D156B4FEA2DD49B1").IsUnique();

            entity.Property(e => e.UsersId).HasColumnName("users_id");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.UsersActivate).HasColumnName("users_activate");
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
