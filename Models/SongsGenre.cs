using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class SongsGenre
{
    public int Id { get; set; }

    public int? SongId { get; set; }

    public int? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual Song? Song { get; set; }
}
