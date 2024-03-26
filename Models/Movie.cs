using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieTitle { get; set; } = null!;

    public string MovieIntro { get; set; } = null!;

    public string MovieTrailer { get; set; } = null!;

    public int ProducerId { get; set; }

    public int ProductId { get; set; }

    public int GenreId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
