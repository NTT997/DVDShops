using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public string? GenreDescriptin { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
