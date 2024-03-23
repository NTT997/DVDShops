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

    public virtual DbSet<Advertisement> Advertisements { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

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

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertisement>(entity =>
        {
            entity.HasKey(e => e.AdsId).HasName("PK__Advertis__DF72100897F861D6");

            entity.ToTable("Advertisement");

            entity.Property(e => e.AdsId)
                .ValueGeneratedNever()
                .HasColumnName("ads_id");
            entity.Property(e => e.AdsDetails)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ads_details");
            entity.Property(e => e.AdsImage).HasColumnName("ads_image");
            entity.Property(e => e.AdsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ads_name");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__B0E1DDB231DED4B1");

            entity.Property(e => e.AlbumId)
                .ValueGeneratedNever()
                .HasColumnName("album_id");
            entity.Property(e => e.AlbumIntro)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("album_intro");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.FbId).HasColumnName("fb_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.SoldUnit).HasColumnName("sold_unit");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_artist");

            entity.HasOne(d => d.Fb).WithMany(p => p.Albums)
                .HasForeignKey(d => d.FbId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_feedback");

            entity.HasOne(d => d.Prod).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_producer");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__6CD0400191595E9F");

            entity.Property(e => e.ArtistId)
                .ValueGeneratedNever()
                .HasColumnName("artist_id");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
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

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FbId).HasName("PK__Feedback__A81DB82DBF66BD2C");

            entity.Property(e => e.FbId)
                .ValueGeneratedNever()
                .HasColumnName("fb_id");
            entity.Property(e => e.FbDate).HasColumnName("fb_date");
            entity.Property(e => e.FbDetail)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("fb_detail");
            entity.Property(e => e.FbReplay)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("fb_replay");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_product");

            entity.HasOne(d => d.Users).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_feedback_member");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__FFE11FCFECCC3D8D");

            entity.Property(e => e.GameId)
                .ValueGeneratedNever()
                .HasColumnName("game_id");
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
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.Games)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_genre");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_product");

            entity.HasOne(d => d.Prod).WithMany(p => p.Games)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_game_producer");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__18428D424F11AAAC");

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("genre_id");
            entity.Property(e => e.GenreDescriptin)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("genre_descriptin");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvId).HasName("PK__Invoices__A8749C29E708C31F");

            entity.Property(e => e.InvId)
                .ValueGeneratedNever()
                .HasColumnName("inv_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_order");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__83CDF7498D1882F1");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");
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
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_genre");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Movies)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_product");

            entity.HasOne(d => d.Prod).WithMany(p => p.Movies)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie_producer");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD88284C88D");

            entity.Property(e => e.NewsId)
                .ValueGeneratedNever()
                .HasColumnName("news_id");
            entity.Property(e => e.NewsContent)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("news_content");
            entity.Property(e => e.NewsImage).HasColumnName("news_image");
            entity.Property(e => e.NewsSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("news_source");
            entity.Property(e => e.NewsTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("news_title");
            entity.Property(e => e.PublishDate).HasColumnName("publish_date");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229652023D4");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.InvId).HasColumnName("inv_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderQuantity).HasColumnName("order_quantity");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Inv).WithMany(p => p.Orders)
                .HasForeignKey(d => d.InvId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_invoice");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_product");

            entity.HasOne(d => d.Shipment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_shipment");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_user");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__Producer__56958AB2114F4416");

            entity.Property(e => e.ProdId)
                .ValueGeneratedNever()
                .HasColumnName("prod_id");
            entity.Property(e => e.ProdIntro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("prod_intro");
            entity.Property(e => e.ProdName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("prod_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Products__82E06B91DBDD7520");

            entity.Property(e => e.PId)
                .ValueGeneratedNever()
                .HasColumnName("p_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.PromoId).HasColumnName("promo_id");
            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Album).WithMany(p => p.Products)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_album");

            entity.HasOne(d => d.Game).WithMany(p => p.Products)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_game");

            entity.HasOne(d => d.Genre).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_genre");

            entity.HasOne(d => d.Prod).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_producer");

            entity.HasOne(d => d.Promo).WithMany(p => p.Products)
                .HasForeignKey(d => d.PromoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_promotion");

            entity.HasOne(d => d.Song).WithMany(p => p.Products)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_song");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_supplier");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromoId).HasName("PK__Promotio__84EB4CA50521708E");

            entity.Property(e => e.PromoId)
                .ValueGeneratedNever()
                .HasColumnName("promo_id");
            entity.Property(e => e.PromoExpireDate).HasColumnName("promo_expire_date");
            entity.Property(e => e.PromoName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("promo_name");
            entity.Property(e => e.PromoPercent).HasColumnName("promo_percent");
            entity.Property(e => e.PromoStartDate).HasColumnName("promo_start_date");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PK__Shipment__41466E599AA94B05");

            entity.Property(e => e.ShipmentId)
                .ValueGeneratedNever()
                .HasColumnName("shipment_id");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ShipmentDate).HasColumnName("shipment_date");

            entity.HasOne(d => d.Order).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_order");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__A535AE1C4735386C");

            entity.Property(e => e.SongId)
                .ValueGeneratedNever()
                .HasColumnName("song_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Intro)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("intro");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.SoldUnit).HasColumnName("sold_unit");
            entity.Property(e => e.SongName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("song_name");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("fk_song_album");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_artist");

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_genre");

            entity.HasOne(d => d.Prod).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_song_producer");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E89C12669C");

            entity.Property(e => e.SupplierId)
                .ValueGeneratedNever()
                .HasColumnName("supplier_id");
            entity.Property(e => e.SupplierAddress).HasColumnName("supplier_address");
            entity.Property(e => e.SupplierEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("supplier_email");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("supplier_name");
            entity.Property(e => e.SupplierPhone).HasColumnName("supplier_phone");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__Users__EAA7D14B68A42504");

            entity.Property(e => e.UsersId)
                .ValueGeneratedNever()
                .HasColumnName("users_id");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.UsersActivate).HasColumnName("users_activate");
            entity.Property(e => e.UsersEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("users_email");
            entity.Property(e => e.UsersName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("users_name");
            entity.Property(e => e.UsersPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("users_password");
            entity.Property(e => e.UsersPhone).HasColumnName("users_phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
