using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string GameTitle { get; set; } = null!;

    public string GameDescription { get; set; } = null!;

    public string GameTrailer { get; set; } = null!;

    public float Price { get; set; }

    public int ProdId { get; set; }

    public int PId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Product PIdNavigation { get; set; } = null!;

    public virtual Producer Prod { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
