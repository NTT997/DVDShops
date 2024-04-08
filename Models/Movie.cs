using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieTitle { get; set; } = null!;

    public string MovieCover { get; set; } = null!;

    public string MovieDescription { get; set; } = null!;

    public string MovieTrailer { get; set; } = null!;

    public string? DownloadLink { get; set; }

    public int ProducerId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<MoviesGenre> MoviesGenres { get; set; } = new List<MoviesGenre>();

    public virtual Producer Producer { get; set; } = null!;
}
