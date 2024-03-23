using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string SongName { get; set; } = null!;

    public float Price { get; set; }

    public string? Intro { get; set; }

    public int SoldUnit { get; set; }

    public int ProdId { get; set; }

    public int GenreId { get; set; }

    public int ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Producer Prod { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
