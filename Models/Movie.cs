using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieTitle { get; set; } = null!;

    public string MovieIntro { get; set; } = null!;

    public string MovieTrailer { get; set; } = null!;

    public int ProdId { get; set; }

    public int PId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Product PIdNavigation { get; set; } = null!;

    public virtual Producer Prod { get; set; } = null!;
}
