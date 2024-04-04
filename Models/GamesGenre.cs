using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class GamesGenre
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public int GenreId { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
