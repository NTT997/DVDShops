using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public int GenreId { get; set; }

    public int SupplierId { get; set; }

    public int ProducerId { get; set; }

    public int? AlbumId { get; set; }

    public int? SongId { get; set; }

    public int PromotionId { get; set; }

    public int? FeedbackId { get; set; }

    public int? GameId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Feedback? Feedback { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Game? Game { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual Producer Producer { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;

    public virtual Song? Song { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
