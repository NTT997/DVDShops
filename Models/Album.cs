using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public int ProducerId { get; set; }

    public int ArtistId { get; set; }

    public string? AlbumIntroduction { get; set; }

    public float? AlbumPrice { get; set; }

    public DateOnly? IssueDate { get; set; }

    public int SoldUnit { get; set; }

    public int? FeedbackId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Feedback? Feedback { get; set; }

    public virtual Producer Producer { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
