using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? ArtistName { get; set; }

    public string? Bio { get; set; }

    public int GenreId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
