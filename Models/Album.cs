using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public int ProdId { get; set; }

    public int ArtistId { get; set; }

    public string? AlbumIntro { get; set; }

    public float? Price { get; set; }

    public DateOnly? IssueDate { get; set; }

    public int SoldUnit { get; set; }

    public int FbId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Feedback Fb { get; set; } = null!;

    public virtual Producer Prod { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
