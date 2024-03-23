using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Product
{
    public int PId { get; set; }

    public int GenreId { get; set; }

    public int SupplierId { get; set; }

    public int ProdId { get; set; }

    public int AlbumId { get; set; }

    public int SongId { get; set; }

    public int PromoId { get; set; }

    public int GameId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Producer Prod { get; set; } = null!;

    public virtual Promotion Promo { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
