using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string GameTitle { get; set; } = null!;

    public string GameCover { get; set; } = null!;

    public string GameDescription { get; set; } = null!;

    public string GameTrailer { get; set; } = null!;

    public string? DownloadLink { get; set; }

    public int ProducerId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<GamesGenre> GamesGenres { get; set; } = new List<GamesGenre>();

    public virtual Producer Producer { get; set; } = null!;
}
