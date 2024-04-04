using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; } = null!;

    public string? ArtistBiography { get; set; }

    public string? ArtistPhoto { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<ArtistsGenre> ArtistsGenres { get; set; } = new List<ArtistsGenre>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
