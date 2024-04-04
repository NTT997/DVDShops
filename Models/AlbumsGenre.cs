using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class AlbumsGenre
{
    public int Id { get; set; }

    public int? AlbumId { get; set; }

    public int? GenreId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Genre? Genre { get; set; }
}
