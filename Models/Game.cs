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

    public int ProducerId { get; set; }

    public int? ProductId { get; set; }

    public int GenreId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
