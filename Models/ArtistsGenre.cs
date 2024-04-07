using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class ArtistsGenre
{
    public int Id { get; set; }

    public int? ArtistId { get; set; }

    public int? GenreId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Genre? Genre { get; set; }
}
