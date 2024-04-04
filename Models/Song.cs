using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string SongName { get; set; } = null!;

    public string? SongIntroduction { get; set; }

    public string? DownloadLink { get; set; }

    public int ProducerId { get; set; }

    public int ArtistId { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<AlbumsSong> AlbumsSongs { get; set; } = new List<AlbumsSong>();

    public virtual Artist Artist { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;

    public virtual ICollection<SongsGenre> SongsGenres { get; set; } = new List<SongsGenre>();
}
