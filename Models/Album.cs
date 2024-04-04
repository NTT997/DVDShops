using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public string AlbumName { get; set; } = null!;

    public int ProducerId { get; set; }

    public int ArtistId { get; set; }

    public string? AlbumIntroduction { get; set; }

    public DateOnly IssueDate { get; set; }

    public int CategoryId { get; set; }

    public int? ReviewId { get; set; }

    public virtual ICollection<AlbumsGenre> AlbumsGenres { get; set; } = new List<AlbumsGenre>();

    public virtual ICollection<AlbumsSong> AlbumsSongs { get; set; } = new List<AlbumsSong>();

    public virtual Artist Artist { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Review? Review { get; set; }
}
