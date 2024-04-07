using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public string? GenreDescription { get; set; }

    public virtual ICollection<AlbumsGenre> AlbumsGenres { get; set; } = new List<AlbumsGenre>();

    public virtual ICollection<ArtistsGenre> ArtistsGenres { get; set; } = new List<ArtistsGenre>();

    public virtual ICollection<GamesGenre> GamesGenres { get; set; } = new List<GamesGenre>();

    public virtual ICollection<MoviesGenre> MoviesGenres { get; set; } = new List<MoviesGenre>();

    public virtual ICollection<SongsGenre> SongsGenres { get; set; } = new List<SongsGenre>();
}
