using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string SongName { get; set; } = null!;

    public float SongPrice { get; set; }

    public string? Intro { get; set; }

    public int SoldUnit { get; set; }

    public int ProducerId { get; set; }

    public int GenreId { get; set; }

    public int ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public int CategoryId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
