using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class AlbumsSong
{
    public int Id { get; set; }

    public int? AlbumId { get; set; }

    public int? SongId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Song? Song { get; set; }
}
